using DAL.Entities;
using DAL.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PlanDetailService
    {
        private readonly PlanRepository _planRepo;
        private readonly PlanDetailRepository _planDetailRepo;
        private readonly PhaseRepository _phaseRepo;
        private readonly MemberRepository _memberRepo;
        public PlanDetailService(PlanRepository planRepo, PlanDetailRepository planDetailRepo, PhaseRepository phaseRepo, MemberRepository memberRepo)
        {
            _planRepo = planRepo;
            _planDetailRepo = planDetailRepo;
            _phaseRepo = phaseRepo;
            _memberRepo = memberRepo;
        }

        public async Task<bool> AddOrUpdatePlanDetail(int accountId, int todayCigarettes, DateOnly? date = null)
        {
            if (todayCigarettes < 0) throw new Exception("Số điếu không được âm");
            var member = await _memberRepo.GetMemberByAccountId(accountId);
            if (member == null) return false;
            var plan = await _planRepo.GetByMemberId(member.MemberId);
            if (plan == null) return false;
            var planId = plan.PlanId;
            var planDate = date ?? DateOnly.FromDateTime(DateTime.Now);
            var planDetail = await _planDetailRepo.GetByPlanIdAndDate(planId, planDate);
            int maxCigarettes = CalculateMaxCigarettes(member, plan, planDate);
            bool isSuccess = todayCigarettes <= maxCigarettes;
            if (planDetail == null)
            {
                planDetail = new PlanDetail
                {
                    PlanId = planId,
                    TodayCigarettes = todayCigarettes,
                    MaxCigarettes = maxCigarettes,
                    Date = planDate,
                    IsSuccess = isSuccess
                };
                await _planDetailRepo.Create(planDetail);
            }
            else
            {
                planDetail.TodayCigarettes = todayCigarettes;
                planDetail.MaxCigarettes = maxCigarettes;
                planDetail.IsSuccess = isSuccess;
                await _planDetailRepo.Update(planDetail);
            }
            // Cập nhật SaveMoney, CigarettesQuit
            plan.SaveMoney = (plan.SaveMoney ?? 0) + (member.CigarettesPerDay.Value - todayCigarettes) * member.CostPerCigarette.Value;
            plan.CigarettesQuit = (plan.CigarettesQuit ?? 0) + (member.CigarettesPerDay.Value - todayCigarettes);
            await _planRepo.Update(plan);
            // Cập nhật trạng thái phase
            await UpdatePhaseStatus(plan, planDate);
            return true;
        }

        private int CalculateMaxCigarettes(Member member, Plan plan, DateOnly date)
        {
            int cigarettesPerDay = member.CigarettesPerDay ?? 0;
            int goalTime = member.GoalTime ?? 0;
            double a = (double)cigarettesPerDay / ((goalTime / 5.0) * 4.0);
            int clock = (date.DayNumber - plan.Clock.Value.Date.DayOfYear) + 1;
            int maxCigarettes = (int)Math.Floor(cigarettesPerDay - a * clock);
            return Math.Max(maxCigarettes, 0);
        }

        private async Task UpdatePhaseStatus(Plan plan, DateOnly date)
        {
            var phases = await _phaseRepo.GetByPlanId(plan.PlanId);
            bool needUpdateNextPhases = false;
            int failPhaseIndex = -1;
            for (int i = 0; i < phases.Count; i++)
            {
                var phase = phases[i];
                if (phase.StartDatePhase.HasValue && phase.EndDatePhase.HasValue)
                {
                    if (date < phase.StartDatePhase.Value)
                        phase.StatusPhase = "Chưa hoàn thành";
                    else if (date > phase.EndDatePhase.Value)
                    {
                        int failDays = await CountFailDays(plan.PlanId, phase.StartDatePhase.Value, phase.EndDatePhase.Value);
                        int totalDays = (phase.EndDatePhase.Value.DayNumber - phase.StartDatePhase.Value.DayNumber + 1);
                        if (failDays > 0.2 * totalDays)
                        {
                            phase.StatusPhase = "Đã thất bại";
                            needUpdateNextPhases = true;
                            failPhaseIndex = i;
                        }
                        else
                            phase.StatusPhase = "Đã hoàn thành";
                    }
                    else
                    {
                        int failDays = await CountFailDays(plan.PlanId, phase.StartDatePhase.Value, date);
                        int totalDays = (date.DayNumber - phase.StartDatePhase.Value.DayNumber + 1);
                        if (failDays > 0.2 * (phase.EndDatePhase.Value.DayNumber - phase.StartDatePhase.Value.DayNumber + 1))
                        {
                            phase.StatusPhase = "Đã thất bại";
                            needUpdateNextPhases = true;
                            failPhaseIndex = i;
                        }
                        else
                            phase.StatusPhase = "Đang thực hiện";
                    }
                    await _phaseRepo.Update(phase);
                }
            }
            // Nếu có phase bị fail, cập nhật lại các phase sau
            if (needUpdateNextPhases && failPhaseIndex >= 0 && failPhaseIndex < phases.Count - 1)
            {
                int goalTime = plan.Member?.GoalTime ?? 0;
                int timePerPhase = goalTime / 5;
                DateOnly newStart = phases[failPhaseIndex].EndDatePhase!.Value.AddDays(1);
                for (int j = failPhaseIndex + 1; j < phases.Count; j++)
                {
                    phases[j].StartDatePhase = newStart;
                    phases[j].EndDatePhase = newStart.AddDays(timePerPhase - 1);
                    phases[j].StatusPhase = "Chưa hoàn thành";
                    await _phaseRepo.Update(phases[j]);
                    newStart = phases[j].EndDatePhase.Value.AddDays(1);
                }
            }
        }

        private async Task<int> CountFailDays(int planId, DateOnly from, DateOnly to)
        {
            var details = await _planDetailRepo.GetByPlanId(planId);
            int fail = 0;
            for (var d = from; d <= to; d = d.AddDays(1))
            {
                var detail = details.FirstOrDefault(x => x.Date == d);
                if (detail == null)
                {
                    // Nếu không nhập thì mặc định là CigarettesPerDay
                    fail++;
                }
                else if (detail.IsSuccess == false)
                {
                    fail++;
                }
            }
            return fail;
        }
    }
} 
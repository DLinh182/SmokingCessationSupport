using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BLL.Services
{
    public class PlanService
    {
        private readonly PlanRepository _planRepo;
        private readonly PhaseRepository _phaseRepo;
        private readonly PlanDetailRepository _planDetailRepo;
        private readonly MemberRepository _memberRepo;
        public PlanService(PlanRepository planRepo, PhaseRepository phaseRepo, PlanDetailRepository planDetailRepo, MemberRepository memberRepo)
        {
            _planRepo = planRepo;
            _phaseRepo = phaseRepo;
            _planDetailRepo = planDetailRepo;
            _memberRepo = memberRepo;
        }

        public async Task<Plan?> CreatePlanForMember(int accountId)
        {
            var member = await _memberRepo.GetMemberByAccountId(accountId);
            if (member == null) return null;
            if (!member.CigarettesPerDay.HasValue || string.IsNullOrEmpty(member.SmokingTime) || !member.GoalTime.HasValue || string.IsNullOrEmpty(member.Reason) || !member.CostPerCigarette.HasValue || string.IsNullOrEmpty(member.MedicalHistory) || string.IsNullOrEmpty(member.MostSmokingTime))
                return null;
            // Nếu đã có plan thì không tạo mới
            var oldPlan = await _planRepo.GetByMemberId(member.MemberId);
            if (oldPlan != null) return oldPlan;

            int cigarettesPerDay = member.CigarettesPerDay.Value;
            int goalTime = member.GoalTime.Value;
            decimal costPerCigarette = member.CostPerCigarette.Value;
            DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly quitSmokingDate = startDate.AddDays(goalTime);
            double a = (double)cigarettesPerDay / ((goalTime / 5.0) * 4.0);
            int maxCigarettes = cigarettesPerDay;

            var plan = new Plan
            {
                MemberId = member.MemberId,
                QuitSmokingDate = quitSmokingDate,
                SaveMoney = 0,
                Clock = DateTime.Now,
                CigarettesQuit = 0,
                MaxCigarettes = maxCigarettes
            };
            plan = await _planRepo.Create(plan);

            // Chia 5 phase
            int timePerPhase = goalTime / 5;
            var phases = new List<Phase>();
            DateOnly phaseStart = startDate;
            for (int i = 1; i <= 5; i++)
            {
                DateOnly phaseEnd = phaseStart.AddDays(timePerPhase - 1);
                phases.Add(new Phase
                {
                    PlanId = plan.PlanId,
                    PhaseNumber = i,
                    StartDatePhase = phaseStart,
                    EndDatePhase = phaseEnd,
                    StatusPhase = i == 1 ? "Đang thực hiện" : "Chưa hoàn thành"
                });
                phaseStart = phaseEnd.AddDays(1);
            }
            await _phaseRepo.CreatePhases(phases);

            // Tạo PlanDetail ngày đầu tiên
            await _planDetailRepo.Create(new PlanDetail
            {
                PlanId = plan.PlanId,
                TodayCigarettes = null,
                MaxCigarettes = maxCigarettes,
                Date = startDate,
                IsSuccess = null
            });
            return plan;
        }

        public async Task<Plan?> GetPlanByAccountId(int accountId)
        {
            var member = await _memberRepo.GetMemberByAccountId(accountId);
            if (member == null) return null;
            return await _planRepo.GetByMemberId(member.MemberId);
        }

        public async Task<bool> IsPlanFailed(int accountId)
        {
            var plan = await GetPlanByAccountId(accountId);
            if (plan == null) return false;
            int failPhase = plan.Phases.Count(p => p.StatusPhase == "Đã thất bại");
            return failPhase > 2;
        }
    }
} 
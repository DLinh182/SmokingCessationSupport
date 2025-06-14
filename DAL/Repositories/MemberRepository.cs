using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
//sdfghjkl
namespace DAL.Repositories
{
    public class MemberRepository(SmokingCessationContext _context)
    {
        //CRUD
        //private SmokingCessationContext _context = null!; //bao voi may rang, cho nay chac chan khong null


        public async Task<List<Member>> GetAll()
        {
            //_context = new SmokingCessationContext();
            return await _context.Members.ToListAsync();
        }

        public async Task<Member?> GetMemberByAccountId(int accountId)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.AccountId == accountId);
        }

        public async Task<bool> UpdateFeedback(int accountId, string? content, int? rating)
        {
            var member = await GetMemberByAccountId(accountId);
            if (member == null) return false;
            // Chỉ cho phép 1 feedback, nếu đã có thì chỉ cho sửa
            member.FeedbackContent = content;
            member.FeedbackRating = rating;
            member.FeedbackDate = DateOnly.FromDateTime(DateTime.Now);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Create(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMemberForm(int accountId, int cigarettesPerDay, string smokingTime, int goalTime, string reason, decimal costPerCigarette, string medicalHistory, string mostSmokingTime)
        {
            var member = await GetMemberByAccountId(accountId);
            if (member == null)
            {
                member = new Member
                {
                    AccountId = accountId,
                    CigarettesPerDay = cigarettesPerDay,
                    SmokingTime = smokingTime,
                    GoalTime = goalTime,
                    Reason = reason,
                    CostPerCigarette = costPerCigarette,
                    MedicalHistory = medicalHistory,
                    MostSmokingTime = mostSmokingTime
                };
                _context.Members.Add(member);
            }
            else
            {
                member.CigarettesPerDay = cigarettesPerDay;
                member.SmokingTime = smokingTime;
                member.GoalTime = goalTime;
                member.Reason = reason;
                member.CostPerCigarette = costPerCigarette;
                member.MedicalHistory = medicalHistory;
                member.MostSmokingTime = mostSmokingTime;
                _context.Members.Update(member);
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

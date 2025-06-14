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

    }
}

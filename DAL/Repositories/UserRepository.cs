using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository(SmokingCessationContext _context)
    {
        //private SmokingCessationContext _context = null!;

        public async Task<User?> GetUserByAccId(int accId)
        {
            _context = new SmokingCessationContext();
            //user.Accountid
            //user.Account.Email
            return await _context.Users
                .Include(u => u.Account)
                .Include(u => u.Member)
                .FirstOrDefaultAsync(u => u.AccountId == accId);
        }

        public async Task<bool> UpdateUserProfile(int accId, string? fullName, string? phoneNumber, DateOnly? birthday, bool? sex)
        {
            _context = new SmokingCessationContext();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.AccountId == accId);
            if (user == null) return false;
            user.FullName = fullName;
            user.PhoneNumber = phoneNumber;
            user.Birthday = birthday;
            user.Sex = sex;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

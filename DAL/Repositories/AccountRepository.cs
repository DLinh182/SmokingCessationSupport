using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class AccountRepository(SmokingCessationContext _context)
    {
        public async Task<Account?> GetAccountByEmail(string email)
        {
            return await _context.Accounts
                                  .Include(a => a.User) // Include User để lấy thông tin Role
                                   .SingleOrDefaultAsync(a => a.Email == email);
        }
        public async Task<Account?> CreateCoachAsync(string email, string password, string fullName, string phoneNumber, DateOnly birthday, bool sex)
        {
            using var context = new SmokingCessationContext();
            var account = new Account
            {
                Email = email,
                Password = password,
                Status = true,
                User = new User
                {
                    FullName = fullName,
                    PhoneNumber = phoneNumber,
                    Birthday = birthday,
                    Sex = sex,
                    Role = "Coach"
                }
            };
            context.Accounts.Add(account);
            await context.SaveChangesAsync();
            return account;
        }

        public async Task<List<Account>> GetAllCoachAsync()
        {
            using var context = new SmokingCessationContext();
            return await context.Accounts.Include(a => a.User)
                .Where(a => a.User != null && a.User.Role == "Coach")
                .ToListAsync();
        }

        public async Task<bool> UpdateCoachStatusAsync(int accountId, bool status)
        {
            using var context = new SmokingCessationContext();
            var acc = await context.Accounts.Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AccountId == accountId && a.User.Role == "Coach");
            if (acc == null) return false;
            acc.Status = status;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Account>> SearchCoachAsync(string keyword)
        {
            using var context = new SmokingCessationContext();
            return await context.Accounts.Include(a => a.User)
                .Where(a => a.User != null && a.User.Role == "Coach" &&
                    (a.User.FullName.Contains(keyword) || a.Email.Contains(keyword) || a.User.PhoneNumber.Contains(keyword)))
                .ToListAsync();
        }

        public async Task<List<Account>> GetAllMemberAsync()
        {
            using var context = new SmokingCessationContext();
            return await context.Accounts.Include(a => a.User)
                .Where(a => a.User != null && a.User.Role == "Member")
                .ToListAsync();
        }

        public async Task<bool> UpdateMemberStatusAsync(int accountId, bool status)
        {
            using var context = new SmokingCessationContext();
            var acc = await context.Accounts.Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AccountId == accountId && a.User.Role == "Member");
            if (acc == null) return false;
            acc.Status = status;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Account>> SearchMemberAsync(string keyword)
        {
            using var context = new SmokingCessationContext();
            return await context.Accounts.Include(a => a.User)
                .Where(a => a.User != null && a.User.Role == "Member" &&
                    (a.User.FullName.Contains(keyword) || a.Email.Contains(keyword) || a.User.PhoneNumber.Contains(keyword)))
                .ToListAsync();
        }
    }
}

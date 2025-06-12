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
    }
}

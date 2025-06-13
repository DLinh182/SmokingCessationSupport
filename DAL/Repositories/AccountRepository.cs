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
    }
}

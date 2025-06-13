using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class RankingRepository(SmokingCessationContext _context)
    {
        public async Task<List<Ranking>> GetAll()
        {
            return await _context.Rankings.Include(r => r.Member.User.Account)
                .Where(r => r.Member.User.Account.Status == true)
                .ToListAsync();
        }

        public async Task<Ranking?> GetById(int accountId)
        {
            return await _context.Rankings.Include(r => r.Member.User)
                .FirstOrDefaultAsync(r => r.Member.AccountId == accountId && r.Member.User.Account.Status == true);
        }
    }
}

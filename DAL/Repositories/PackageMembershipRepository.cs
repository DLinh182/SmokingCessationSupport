using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PackageMembershipRepository(SmokingCessationContext _context)
    {
        public async Task<List<PackageMembership>> GetAll()
        {
            return await _context.PackageMemberships.ToListAsync();
        }

        public async Task<PackageMembership?> GetById(int id)
        {
            return await _context.PackageMemberships.FindAsync(id);
        }

        public async Task Update(PackageMembership package)
        {
            _context.PackageMemberships.Update(package);
            await _context.SaveChangesAsync();
        }

    }
}

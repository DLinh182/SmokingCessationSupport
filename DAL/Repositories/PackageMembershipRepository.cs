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

        public async Task<PackageMembership> Update(PackageMembership packageMembership)
        {
            _context.PackageMemberships.Update(packageMembership);
            await _context.SaveChangesAsync();
            return packageMembership;
        }
    }
}

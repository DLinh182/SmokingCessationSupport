using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PlatformRepository(SmokingCessationContext _context)
    {
        public async Task<Platform?> GetPlatform()
        {
            return await _context.Platforms.FirstOrDefaultAsync();
        }

        public async Task Update(Platform platform)
        {
            _context.Platforms.Update(platform);
            await _context.SaveChangesAsync();
        }
    }
}

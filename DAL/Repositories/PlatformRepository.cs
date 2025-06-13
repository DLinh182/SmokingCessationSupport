using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PlatformRepository(SmokingCessationContext _context)
    {
        public async Task<List<Platform>> GetAllPlatformsAsync()
        {
            return await _context.Platforms.ToListAsync();
        }
    }
} 
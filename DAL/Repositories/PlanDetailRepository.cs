using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PlanDetailRepository
    {
        private readonly SmokingCessationContext _context;
        public PlanDetailRepository(SmokingCessationContext context)
        {
            _context = context;
        }
        public async Task<PlanDetail?> GetByPlanIdAndDate(int planId, DateOnly date)
        {
            return await _context.PlanDetails.FirstOrDefaultAsync(p => p.PlanId == planId && p.Date == date);
        }
        public async Task<List<PlanDetail>> GetByPlanId(int planId)
        {
            return await _context.PlanDetails.Where(p => p.PlanId == planId).ToListAsync();
        }
        public async Task<PlanDetail> Create(PlanDetail detail)
        {
            _context.PlanDetails.Add(detail);
            await _context.SaveChangesAsync();
            return detail;
        }
        public async Task Update(PlanDetail detail)
        {
            _context.PlanDetails.Update(detail);
            await _context.SaveChangesAsync();
        }
    }
} 
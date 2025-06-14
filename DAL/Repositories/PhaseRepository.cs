using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PhaseRepository
    {
        private readonly SmokingCessationContext _context;
        public PhaseRepository(SmokingCessationContext context)
        {
            _context = context;
        }
        public async Task CreatePhases(List<Phase> phases)
        {
            _context.Phases.AddRange(phases);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Phase phase)
        {
            _context.Phases.Update(phase);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Phase>> GetByPlanId(int planId)
        {
            return await _context.Phases.Where(p => p.PlanId == planId).ToListAsync();
        }
    }
} 
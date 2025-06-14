using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PlanRepository
    {
        private readonly SmokingCessationContext _context;
        public PlanRepository(SmokingCessationContext context)
        {
            _context = context;
        }
        public async Task<Plan?> GetByMemberId(int memberId)
        {
            return await _context.Plans.Include(p => p.Phases).Include(p => p.PlanDetails).FirstOrDefaultAsync(p => p.MemberId == memberId);
        }
        public async Task<Plan> Create(Plan plan)
        {
            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();
            return plan;
        }
        public async Task Update(Plan plan)
        {
            _context.Plans.Update(plan);
            await _context.SaveChangesAsync();
        }
        public async Task<Plan?> GetById(int planId)
        {
            return await _context.Plans.Include(p => p.Phases).Include(p => p.PlanDetails).FirstOrDefaultAsync(p => p.PlanId == planId);
        }
    }
} 
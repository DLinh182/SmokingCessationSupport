using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CommentRepository(SmokingCessationContext _context)
    {
        public async Task<List<Comment>> GetAll()
        {
            return await _context.Comments.Include(c => c.Account!.User).ToListAsync();
        }

    }
}

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
        public async Task Add(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Comment?> GetById(int commentId)
        {
            return await _context.Comments
                .Include(c => c.Account!.User)
                .FirstOrDefaultAsync(c => c.CmtId == commentId);
        }

    }
}

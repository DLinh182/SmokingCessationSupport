using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.Repositories
{
    public class CommunityPostRepository(SmokingCessationContext _context)
    {
        public async Task<List<CommunityPost>> GetAll()
        {
            return await _context.CommunityPosts.Include(a => a.Account.User).ToListAsync();
        }

        public async Task Add(CommunityPost post)
        {
            _context.CommunityPosts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CommunityPost>> SearchByNameOrContent(string keyword)
        {
            return await _context.CommunityPosts
                .Include(a => a.Account.User)
                .Where(p => p.Content.Contains(keyword) || p.Account.User.FullName.Contains(keyword))
                .ToListAsync();
        }

        public async Task Delete(int postId)
        {
            var post = await _context.CommunityPosts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.PostId == postId);
            if (post != null)
            {
                // Remove related comments if necessary
                _context.Comments.RemoveRange(post.Comments);
                _context.CommunityPosts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CommunityPost?> GetById(int postId)
        {
            var post = await _context.CommunityPosts.FindAsync(postId);
            return post;
        }
    }
}

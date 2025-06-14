using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.RequestDTO;
using BLL.DTOs.ResponseDTO;
using DAL.Repositories;
using DAL.Entities;

namespace BLL.Services
{
    public class CommentService(CommentRepository _repo)
    {
        public async Task<List<CommentResponseDTO>> GetAllCommentsAsync(int postId)
        {
            var comments = await _repo.GetAll();
            List<CommentResponseDTO> result = new List<CommentResponseDTO>();
            foreach (var comment in comments)
            {
                if (comment.PostId == postId)
                {
                    result.Add(new CommentResponseDTO
                    {
                        FullName = comment.Account!.User!.FullName,
                        Content = comment.Comment1,
                        CreateTime = comment.CreateTime
                    });
                }

            }
            return result;
        }

        public async Task AddCommentAsync(int postId, CommentCreateRequestDTO dto, int accountId)
        {
            var newComment = new Comment
            {
                PostId = postId,
                Comment1 = dto.Comment,
                AccountId = accountId,
                CreateTime = DateTime.Now
            };
            await _repo.Add(newComment);
        }

        public async Task<bool> DeleteCommentAsync(int commentId, int accountId, bool isAdmin)
        {
            var comment = await _repo.GetById(commentId);
            if (comment == null) return false;

            // Giả sử AccountId là string, nếu khác sửa lại cho phù hợp
            if (isAdmin || comment.AccountId == accountId)
            {
                await _repo.Delete(commentId);
                return true;
            }
            return false;
        }
    }
}

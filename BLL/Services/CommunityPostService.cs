using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.RequestDTO;
using BLL.DTOs.ResponseDTO;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class CommunityPostService(CommunityPostRepository _repo)
    {
        public async Task<List<CommunityPostResponseDTO>> GetAllCommunityPost()
        {
            var posts = await _repo.GetAll();
            List<CommunityPostResponseDTO> rs = new List<CommunityPostResponseDTO>();
            foreach (var post in posts)
            {
                rs.Add(new CommunityPostResponseDTO
                {
                    FullName = post.Account!.User!.FullName!,
                    Content = post.Content,
                    CreateTime = post.CreateTime
                });
            }
            return rs;
        }

        public async Task AddCommunityPost(CommunityPostRequestDTO post, int accountId)
        {
            var newPost = new CommunityPost
            {
                AccountId = accountId,
                Content = post.Content,
                CreateTime = DateOnly.FromDateTime(DateTime.Now)
            };
            await _repo.Add(newPost);
        }

        public async Task<List<CommunityPostResponseDTO>> SearchCommunityPostByNameOrContent(string keyword)
        {
            var posts = await _repo.SearchByNameOrContent(keyword);
            List<CommunityPostResponseDTO> rs = new List<CommunityPostResponseDTO>();
            foreach (var post in posts)
            {
                rs.Add(new CommunityPostResponseDTO
                {
                    FullName = post.Account!.User!.FullName!,
                    Content = post.Content,
                    CreateTime = post.CreateTime
                });
            }
            return rs;
        }

        //public async Task DeleteCommunityPost(int postId)
        //{
        //    await _repo.Delete(postId);
        //}
        public async Task<bool> DeleteCommunityPostAsync(int postId, int accountId, bool isAdmin)
        {
            var post = await _repo.GetById(postId);
            if (post == null) return false;

            if (isAdmin || post.AccountId == accountId)
            {
                await _repo.Delete(postId);
                return true;
            }
            return false;
        }

        public async Task<CommunityPost?> GetPostById(int postId)
        {
            return await _repo.GetById(postId);
        }
    }
}

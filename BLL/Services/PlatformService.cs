using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.ResponseDTO;
using BLL.DTOs.RequestDTO;
using DAL.Repositories;

namespace BLL.Services
{
    public class PlatformService(PlatformRepository _repo)
    {
        public async Task<PlatformResponseDTO> GetPlatform()
        {
            var x = await _repo.GetPlatform();
            if (x == null) return null;
            return new PlatformResponseDTO
            {
                News1Title = x.News1Title,
                News1Content = x.News1Content,
                News1Link = x.News1Link,
                News2Title = x.News2Title,
                News2Content = x.News2Content,
                News2Link = x.News2Link,
                News3Title = x.News3Title,
                News3Content = x.News3Content,
                News3Link = x.News3Link,
                Message = x.Message,
                About = x.About,
                Benefit = x.Benefit
            };
        }

        public async Task UpdatePlatformAsync(PlatformUpdateRequestDTO request)
        {
            var platform = await _repo.GetPlatform();
            if (platform == null)
            {
                throw new KeyNotFoundException("Platform not found");
            }
            if (request.News1Title != null) platform.News1Title = request.News1Title;
            if (request.News1Content != null) platform.News1Content = request.News1Content;
            if (request.News1Link != null) platform.News1Link = request.News1Link;
            if (request.News2Title != null) platform.News2Title = request.News2Title;
            if (request.News2Content != null) platform.News2Content = request.News2Content;
            if (request.News2Link != null) platform.News2Link = request.News2Link;
            if (request.News3Title != null) platform.News3Title = request.News3Title;
            if (request.News3Content != null) platform.News3Content = request.News3Content;
            if (request.News3Link != null) platform.News3Link = request.News3Link;
            if (request.Message != null) platform.Message = request.Message;
            if (request.About != null) platform.About = request.About;
            if (request.Benefit != null) platform.Benefit = request.Benefit;
            await _repo.Update(platform);
        }
    }
}

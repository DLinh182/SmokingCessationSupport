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
            if (!string.IsNullOrWhiteSpace(request.News1Title) && request.News1Title != "string")
                platform.News1Title = request.News1Title;
            if (!string.IsNullOrWhiteSpace(request.News1Content) && request.News1Content != "string")
                platform.News1Content = request.News1Content;
            if (!string.IsNullOrWhiteSpace(request.News1Link) && request.News1Link != "string")
                platform.News1Link = request.News1Link;

            if (!string.IsNullOrWhiteSpace(request.News2Title) && request.News2Title != "string")
                platform.News2Title = request.News2Title;
            if (!string.IsNullOrWhiteSpace(request.News2Content) && request.News2Content != "string")
                platform.News2Content = request.News2Content;
            if (!string.IsNullOrWhiteSpace(request.News2Link) && request.News2Link != "string")
                platform.News2Link = request.News2Link;

            if (!string.IsNullOrWhiteSpace(request.News3Title) && request.News3Title != "string")
                platform.News3Title = request.News3Title;
            if (!string.IsNullOrWhiteSpace(request.News3Content) && request.News3Content != "string")
                platform.News3Content = request.News3Content;
            if (!string.IsNullOrWhiteSpace(request.News3Link) && request.News3Link != "string")
                platform.News3Link = request.News3Link;

            if (!string.IsNullOrWhiteSpace(request.Message) && request.Message != "string")
                platform.Message = request.Message;
            if (!string.IsNullOrWhiteSpace(request.About) && request.About != "string")
                platform.About = request.About;
            if (!string.IsNullOrWhiteSpace(request.Benefit) && request.Benefit != "string")
                platform.Benefit = request.Benefit;
            platform.LastUpdated = DateTime.UtcNow;
            await _repo.Update(platform);
        }
    }
}

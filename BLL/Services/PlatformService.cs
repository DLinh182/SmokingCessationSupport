using DAL.Repositories;
using BLL.DTOs.ResponseDTO;

namespace BLL.Services;

public class PlatformService
{
    private readonly PlatformRepository _platformRepository;
    public PlatformService()
    {
        _platformRepository = new PlatformRepository(null!);
    }

    public async Task<List<PlatformResponseDTO>> GetAllPlatformsAsync()
    {
        var platforms = await _platformRepository.GetAllPlatformsAsync();
        return platforms.Select(p => new PlatformResponseDTO
        {
            News1Title = p.News1Title,
            News1Content = p.News1Content,
            News1Link = p.News1Link,
            News2Title = p.News2Title,
            News2Content = p.News2Content,
            News2Link = p.News2Link,
            News3Title = p.News3Title,
            News3Content = p.News3Content,
            News3Link = p.News3Link,
            Message = p.Message,
            About = p.About,
            Benefit = p.Benefit
        }).ToList();
    }
} 
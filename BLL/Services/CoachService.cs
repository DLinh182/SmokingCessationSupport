using DAL.Repositories;
using BLL.DTOs.RequestDTO;
using BLL.DTOs.ResponseDTO;

namespace BLL.Services;

public class CoachService
{
    private readonly AccountRepository _accountRepository;
    public CoachService(AccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<CoachResponseDTO?> CreateCoachAsync(CoachCreateRequestDTO dto)
    {
        var acc = await _accountRepository.CreateCoachAsync(dto.Email, dto.Password, dto.FullName, dto.PhoneNumber, dto.Birthday, dto.Sex);
        if (acc == null) return null;
        return new CoachResponseDTO
        {
            AccountId = acc.AccountId,
            Email = acc.Email,
            FullName = acc.User.FullName!,
            PhoneNumber = acc.User.PhoneNumber!,
            Birthday = acc.User.Birthday,
            Sex = acc.User.Sex,
            Status = acc.Status,
            Role = acc.User.Role!
        };
    }

    public async Task<List<CoachResponseDTO>> GetAllCoachAsync()
    {
        var list = await _accountRepository.GetAllCoachAsync();
        return list.Select(acc => new CoachResponseDTO
        {
            AccountId = acc.AccountId,
            Email = acc.Email,
            FullName = acc.User.FullName!,
            PhoneNumber = acc.User.PhoneNumber!,
            Birthday = acc.User.Birthday,
            Sex = acc.User.Sex,
            Status = acc.Status,
            Role = acc.User.Role!
        }).ToList();
    }

    public async Task<bool> UpdateCoachStatusAsync(int accountId, bool status)
    {
        return await _accountRepository.UpdateCoachStatusAsync(accountId, status);
    }

    public async Task<List<CoachResponseDTO>> SearchCoachAsync(string keyword)
    {
        var list = await _accountRepository.SearchCoachAsync(keyword);
        return list.Select(acc => new CoachResponseDTO
        {
            AccountId = acc.AccountId,
            Email = acc.Email,
            FullName = acc.User.FullName!,
            PhoneNumber = acc.User.PhoneNumber!,
            Birthday = acc.User.Birthday,
            Sex = acc.User.Sex,
            Status = acc.Status,
            Role = acc.User.Role!
        }).ToList();
    }
} 
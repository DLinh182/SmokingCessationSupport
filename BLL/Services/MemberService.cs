using DAL.Repositories;
using BLL.DTOs.ResponseDTO;

namespace BLL.Services;

public class MemberService
{
    private readonly AccountRepository _accountRepository;
    public MemberService(AccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<List<MemberResponseDTO>> GetAllMemberAsync()
    {
        var list = await _accountRepository.GetAllMemberAsync();
        return list.Select(acc => new MemberResponseDTO
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

    public async Task<bool> UpdateMemberStatusAsync(int accountId, bool status)
    {
        return await _accountRepository.UpdateMemberStatusAsync(accountId, status);
    }

    public async Task<List<MemberResponseDTO>> SearchMemberAsync(string keyword)
    {
        var list = await _accountRepository.SearchMemberAsync(keyword);
        return list.Select(acc => new MemberResponseDTO
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
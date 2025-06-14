using DAL.Repositories;
using BLL.DTOs.ResponseDTO;
using BLL.DTOs.RequestDTO;

namespace BLL.Services;

public class MemberService
{
    private readonly AccountRepository _accountRepository;
    private readonly MemberRepository _memberRepository;
    private readonly PlanService _planService;
    public MemberService(AccountRepository accountRepository, MemberRepository memberRepository, PlanService planService)
    {
        _accountRepository = accountRepository;
        _memberRepository = memberRepository;
        _planService = planService;
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

    public async Task<bool> UpdateMemberForm(int accountId, MemberFormRequestDTO dto)
    {
        var ok = await _memberRepository.UpdateMemberForm(accountId, dto.CigarettesPerDay, dto.SmokingTime, dto.GoalTime, dto.Reason, dto.CostPerCigarette, dto.MedicalHistory, dto.MostSmokingTime);
        if (ok)
        {
            await _planService.CreatePlanForMember(accountId);
        }
        return ok;
    }
} 
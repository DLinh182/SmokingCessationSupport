using DAL.Repositories;
using BLL.DTOs.ResponseDTO;
using BLL.DTOs.RequestDTO;

namespace BLL.Services;

public class UserService
{
    private readonly UserRepository _userRepository;
    public UserService()
    {
        _userRepository = new UserRepository(null!);
    }

    public async Task<UserProfileResponseDTO?> GetUserProfile(int accountId)
    {
        var user = await _userRepository.GetUserByAccId(accountId);
        if (user == null) return null;
        return new UserProfileResponseDTO
        {
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Birthday = user.Birthday,
            Sex = user.Sex,
            Email = user.Account?.Email
        };
    }

    public async Task<bool> UpdateUserProfile(int accountId, UserProfileUpdateRequestDTO dto)
    {
        return await _userRepository.UpdateUserProfile(accountId, dto.FullName, dto.PhoneNumber, dto.Birthday, dto.Sex);
    }
} 
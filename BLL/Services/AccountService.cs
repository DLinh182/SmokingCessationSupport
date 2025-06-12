using System;
using System.Threading.Tasks;
using BLL.DTOs.RequestDTO;
using BLL.DTOs.ResponseDTO;
using BLL.Utils;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class AccountService
    {
        private readonly AccountRepository _accountRepository;
        private readonly UserRepository _userRepo;

        public AccountService(AccountRepository accountRepository, UserRepository userRepo)
        {
            _accountRepository = accountRepository;
            _userRepo = userRepo;
        }

        public async Task<LoginResponseDTO> LoginAync(LoginRequestDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                throw new Exception("Email và Password là cần thiết");
            }

            // Tìm tài khoản theo email
            var acc = await _accountRepository.GetAccountByEmail(request.Email);

            if (acc == null)
            {
                throw new Exception("Không tìm thấy account");
            }

            // TODO: Sử dụng hashing mật khẩu thay vì so sánh trực tiếp trong thực tế
            if (acc.Password != request.Password)
            {
                throw new Exception("Mật khẩu không đúng");
            }

            // Kiểm tra trạng thái tài khoản
            if (!acc.Status) // Giả định Status là bool
            {
                throw new Exception("Tài khoản đã bị vô hiệu hóa");
            }

            // Lấy thông tin user để lấy Role
            User? user = await _userRepo.GetUserByAccId(acc.AccountId);
            if (user == null)
            {
                throw new Exception("Không tìm thấy user tương ứng với account này");
            }

            // Tạo JWT Token
            string token = JwtHelper.GenerateJwtToken(acc);

            // Trả về DTO
            var response = new LoginResponseDTO()
            {
                Token = token,
                Role = user.Role
            };

            return response;
        }
    }
}
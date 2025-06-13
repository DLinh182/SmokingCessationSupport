namespace BLL.DTOs.ResponseDTO;

public class UserProfileResponseDTO
{
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly? Birthday { get; set; }
    public bool? Sex { get; set; }
    public string? Email { get; set; }
} 
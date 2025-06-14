namespace BLL.DTOs.RequestDTO;

public class UserProfileUpdateRequestDTO
{
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly? Birthday { get; set; }
    public bool? Sex { get; set; }
} 
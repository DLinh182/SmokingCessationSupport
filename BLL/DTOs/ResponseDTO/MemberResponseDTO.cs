namespace BLL.DTOs.ResponseDTO;

public class MemberResponseDTO
{
    public int AccountId { get; set; }
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateOnly? Birthday { get; set; }
    public bool? Sex { get; set; }
    public bool Status { get; set; }
    public string Role { get; set; } = null!;
} 
namespace BLL.DTOs.RequestDTO;

public class CoachCreateRequestDTO
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateOnly Birthday { get; set; }
    public bool Sex { get; set; }
} 
namespace BLL.DTOs.RequestDTO;

public class MemberFormRequestDTO
{
    public int CigarettesPerDay { get; set; }
    public string SmokingTime { get; set; } = null!;
    public int GoalTime { get; set; }
    public string Reason { get; set; } = null!;
    public decimal CostPerCigarette { get; set; }
    public string MedicalHistory { get; set; } = null!;
    public string MostSmokingTime { get; set; } = null!;
} 
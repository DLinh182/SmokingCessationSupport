namespace BLL.DTOs.RequestDTO;

public class PlanDetailRequestDTO
{
    public int TodayCigarettes { get; set; }
    public DateOnly? Date { get; set; } // Nếu null thì lấy ngày hôm nay
} 
namespace BLL.DTOs.RequestDTO;

public class FeedbackRequestDTO
{
    public string? FeedbackContent { get; set; }
    public int? FeedbackRating { get; set; } // 1-5
} 
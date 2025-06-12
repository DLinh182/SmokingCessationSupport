using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.ResponseDTO
{
    public class AllFeedbackGetResponse
    {
        public string FullName { get; set; } = null!;
        public string? Feedback_content { get; set; }
        public DateOnly? Feedback_date { get; set; }
        public int? Feedback_rating { get; set; }
    }
}

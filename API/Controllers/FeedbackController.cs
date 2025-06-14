using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //api/Feedback
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController(FeedbackService feedbackService) : ControllerBase
    {//                                  constructor
        //private FeedbackService _feedbackService { get; set; }

        //public FeedbackController(FeedbackService feedbackService)
        //{
        //    _feedbackService = feedbackService;
        //}
        //api/Feedback/
        [HttpGet]
        public async Task<IActionResult> GetAllFeedbackAsync()
        {
            //_feedbackService = new FeedbackService();
            try
            {
                var response = await feedbackService.GetAllFeedback();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedback([FromBody] BLL.DTOs.RequestDTO.FeedbackRequestDTO dto)
        {
            var accountIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (accountIdClaim == null) return Unauthorized();
            int accountId = int.Parse(accountIdClaim.Value);
            // Kiểm tra đã có feedback chưa
            var member = await feedbackService.GetMemberByAccountId(accountId);
            if (member == null) return BadRequest("Không tìm thấy member");
            if (!string.IsNullOrEmpty(member.FeedbackContent) && member.FeedbackRating != null)
                return BadRequest("Bạn đã gửi feedback, hãy dùng PUT để chỉnh sửa!");
            try
            {
                var result = await feedbackService.SubmitOrUpdateFeedback(accountId, dto.FeedbackContent, dto.FeedbackRating);
                if (!result) return BadRequest("Không thể gửi feedback");
                return Ok("Feedback thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeedback([FromBody] BLL.DTOs.RequestDTO.FeedbackRequestDTO dto)
        {
            var accountIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (accountIdClaim == null) return Unauthorized();
            int accountId = int.Parse(accountIdClaim.Value);
            // Kiểm tra đã có feedback chưa
            var member = await feedbackService.GetMemberByAccountId(accountId);
            if (member == null) return BadRequest("Không tìm thấy member");
            if (string.IsNullOrEmpty(member.FeedbackContent) || member.FeedbackRating == null)
                return BadRequest("Bạn chưa có feedback, hãy dùng POST để tạo mới!");
            try
            {
                var result = await feedbackService.SubmitOrUpdateFeedback(accountId, dto.FeedbackContent, dto.FeedbackRating);
                if (!result) return BadRequest("Không thể cập nhật feedback");
                return Ok("Cập nhật feedback thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterFeedbackByRating([FromQuery] int rating)
        {
            var result = await feedbackService.FilterFeedbackByRating(rating);
            return Ok(result);
        }
    }
}

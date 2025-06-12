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
    }
}

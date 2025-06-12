
using BLL.DTOs.ResponseDTO;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class FeedbackService(MemberRepository _memberRepo, UserRepository _userRepo)
    {
    //    private MemberRepository _memberRepo = null!;
    //    private UserRepository _userRepo = null!;

        //service nhận vào request trả về response
        public async Task<List<AllFeedbackGetResponse>> GetAllFeedback()
        {
            //khoi tao repo
            //_memberRepo = new MemberRepository();
            //_userRepo = new UserRepository();
            var memberList = await _memberRepo.GetAll();
            //tao feedback list de return ra
            var feedbackList = new List<AllFeedbackGetResponse>(); //

            //duyệt qua từng member để lấy thông tin trong member
            //để tạo ra feedback tương ứng với member đó
            foreach (var member in memberList)
            {
                //check xem feedback co ton tai chua, neu ton tai thi tao feedback
                if (member.FeedbackContent != null && member.FeedbackDate != null && member.FeedbackRating != null)
                {
                    // lấy user theo account id của member de lay full name
                    User? u = await _userRepo.GetUserByAccId(member.AccountId);
                    if (u == null)
                    {
                        throw new Exception("Account id not found");
                    }
                    //tao feedback response tuong ung voi tung member
                    AllFeedbackGetResponse feedback = new AllFeedbackGetResponse()
                    {
                        FullName = u.FullName!,
                        Feedback_content = member.FeedbackContent,
                        Feedback_date = member.FeedbackDate,
                        Feedback_rating = member.FeedbackRating,
                    };
                    feedbackList.Add(feedback);
                }
            }
            return feedbackList;
        }
    }
}

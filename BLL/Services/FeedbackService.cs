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

        public async Task<bool> SubmitOrUpdateFeedback(int accountId, string? content, int? rating)
        {
            if (rating is < 1 or > 5) throw new Exception("Rating phải từ 1 đến 5");
            return await _memberRepo.UpdateFeedback(accountId, content, rating);
        }

        public async Task<List<AllFeedbackGetResponse>> FilterFeedbackByRating(int rating)
        {
            var all = await GetAllFeedback();
            return all.Where(f => f.Feedback_rating == rating).ToList();
        }

        public async Task<DAL.Entities.Member?> GetMemberByAccountId(int accountId)
        {
            return await _memberRepo.GetMemberByAccountId(accountId);
        }
    }
}

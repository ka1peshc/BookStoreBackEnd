using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreRepository.Repository
{
    public interface IFeedbackRepository
    {
        string connectionString { get; set; }

        string AddNewReview(FeedbackModel fm);
        IEnumerable<FeedbackModel> DisplayOrderList(int bookId);
    }
}
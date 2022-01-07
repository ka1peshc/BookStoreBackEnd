using BookStoreModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreManager.Manager
{
    public interface IFeedbackManager
    {
        IConfiguration Configuration { get; }

        string AddNewReview(FeedbackModel fm);
        IEnumerable<FeedbackModel> DisplayReviewList(int bookId);
    }
}
using BookStoreModels;
using BookStoreRepository.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class FeedbackManager : IFeedbackManager
    {
        private readonly IFeedbackRepository repository;
        public FeedbackManager(IFeedbackRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public string AddNewReview(FeedbackModel fm)
        {
            try
            {
                return this.repository.AddNewReview(fm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<FeedbackModel> DisplayReviewList(int bookId)
        {
            try
            {
                return this.repository.DisplayReviewList(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

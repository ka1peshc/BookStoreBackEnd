using BookStoreManager.Manager;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackManager manager;
        public FeedbackController(IFeedbackManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("feedback/addNewReview")]
        public IActionResult AddNewReview([FromBody] FeedbackModel item)
        {
            try
            {
                string result = this.manager.AddNewReview(item);
                if (result.Equals("Review added successful"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("feedback/displayReviews")]
        public IActionResult DisplayReview(int bookId)
        {
            try
            {
                IEnumerable<FeedbackModel> result = this.manager.DisplayReviewList(bookId);
                if ((int)result.Count() != 0)
                {
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No feedback found" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}

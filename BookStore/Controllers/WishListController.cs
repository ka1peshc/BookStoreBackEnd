using BookStoreManager.Manager;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class WishListController: Controller
    {
        private readonly IWishListManager manager;
        public WishListController(IWishListManager manager)
        {
            this.manager = manager;
        }
        [HttpGet]
        [Route("wishList/displayItems")]
        public IActionResult DisplayWishListItem(int userId)
        {
            try
            {
                IEnumerable<WishListModel> result = this.manager.DisplayCartItem(userId);
                if ((int)result.Count() != 0)
                {
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No notes found" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("wishList/addItem")]
        public IActionResult AddNewItem([FromBody] WishListModel model)
        {
            try
            {
                string result = this.manager.AddNewItemInWishList(model);
                if (result.Equals("Book added to wishlist"))
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
        
        [HttpDelete]
        [Route("wishList/removeItem")]
        public IActionResult RemoveItem(int wishListItemId)
        {
            try
            {
                string result = this.manager.DeleteItemInWishList(wishListItemId);
                if (result.Equals("Book removed from wishlist"))
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
    }
}

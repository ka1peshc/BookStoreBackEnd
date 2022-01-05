using BookStoreManager.Manager;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly ICartManager manager;
        public CartController(ICartManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("Cart/addNewItem")]
        public IActionResult NewBook([FromBody] CartModel item)
        {
            try
            {
                string result = this.manager.AddNewItem(item);
                if (result.Equals("Item added to cart"))
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

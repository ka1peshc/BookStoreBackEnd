using BookStoreManager.Manager;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager manager;
        public OrderController(IOrderManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("order/addNewOrder")]
        public IActionResult NewBook([FromBody] OrderModel item)
        {
            try
            {
                string result = this.manager.AddNewOrder(item);
                if (result.Equals("Order added successful"))
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
        [Route("order/displayOrder")]
        public IActionResult DisplayOrderItem(int userId)
        {
            try
            {
                IEnumerable<OrderModel> result = this.manager.DisplayOrderList(userId);
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

        [HttpGet]
        [Route("order/displaySuccessOrder")]
        public IActionResult DisplaySuccessOrder(int orderId)
        {
            try
            {
                IEnumerable<OrderModel> result = this.manager.DisplayOrderList(orderId);
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
    }
}

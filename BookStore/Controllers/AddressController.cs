using BookStoreManager.Manager;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager manager;
        public AddressController(IAddressManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("Address/addNewAddress")]
        public IActionResult AddNewAddress([FromBody] AddressModel addModel)
        {
            try
            {
                string result = this.manager.AddNewAddress(addModel);
                if (result.Equals("Insert address successful"))
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
        [HttpPut]
        [Route("Address/updateAddress")]
        public IActionResult UpdateAddress([FromBody] AddressModel addModel)
        {
            try
            {
                string result = this.manager.UpdateAddress(addModel);
                if (result.Equals("Update address successful"))
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
        [Route("Cart/displayAddress")]
        public IActionResult DisplayAddress(int userId)
        {
            try
            {
                IEnumerable<AddressModel> result = this.manager.DisplayAddress(userId);
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

using BookStoreManager.Manager;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("api/register")]
        public IActionResult Register([FromBody]UserModel userData)
        {
            try
            {
                string result = this.manager.Register(userData);
                if (result.Equals("Registration Successful"))
                {
                    //this.logger.Info(result + Environment.NewLine + DateTime.Now);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result, Data = " Session data" });
                }
                else
                {
                    //this.logger.Warn(result + Environment.NewLine + DateTime.Now);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                //this.logger.Error(ex.Message + Environment.NewLine + DateTime.Now);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] UserLoginModel userData)
        {
            try
            {
                string result = this.manager.Login(userData);
                if (result.Equals("Login Successful"))
                {
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string fullName = database.StringGet("Full Name");
                    string email = database.StringGet("email");
                    int userId = Convert.ToInt32(database.StringGet("User Id"));
                    UserModel userDetail = new UserModel
                    {
                        userId = userId,
                        userFullName = fullName,
                        userEmail = email
                    };
                    string tokenString = this.manager.GenerateToken(userData.Email);
                    return this.Ok(new { Status = true, Message = result, Data = userDetail, Token = tokenString });
                }
                else
                {
                    //this.logger.Warn(result + Environment.NewLine + DateTime.Now);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                //this.logger.Error(ex.Message + Environment.NewLine + DateTime.Now);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Check if reset password successful
        /// </summary>
        /// <param name="userData">UserLoginModel class</param>
        /// <returns>Http response</returns>
        [HttpPut]
        [Route("api/resetpassword")]
        public IActionResult ResetPassword([FromBody] UserLoginModel userData)
        {
            try
            {
                string result = this.manager.ResetPassword(userData);
                if (result.Equals("Password Update Successful"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result, Data = " Session data" });
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

        /// <summary>
        /// Call forget api using params where key=email and value=mailid
        /// </summary>
        /// <param name="email">Email in string</param>
        /// <returns>Http response</returns>
        [HttpPost]
        [Route("api/forgotpassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                string result = await this.manager.ForgotPassword(email);
                if (result == "Email send Successfully")
                {                    
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result, Data = " Session data" });
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

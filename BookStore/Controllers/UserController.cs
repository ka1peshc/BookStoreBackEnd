using BookStoreManager.Manager;
using BookStoreModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        const string SessionUserFullname = "_Name";
        const string SessionUserEmail = "_Email";
        const string SessionUserId = "_Id";
        private readonly ILogger logger ;
        //public UserController(IUserManager manager, ILogger log)
        //{
        //    this.manager = manager;
        //    logger = log;
        //}
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]UserModel userData)
        {
            try
            {
                string result = this.manager.Register(userData);
                if (result.Equals("Registration Successful"))
                {
                    //logger.LogInformation(result + Environment.NewLine + DateTime.Now);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result, Data = " Session data" });
                }
                else
                {
                    //logger.LogWarning(result + Environment.NewLine + DateTime.Now);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                //logger.LogError(ex.Message + Environment.NewLine + DateTime.Now);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("login")]
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
                    HttpContext.Session.SetString(SessionUserFullname, fullName);
                    HttpContext.Session.SetString(SessionUserEmail, email);
                    HttpContext.Session.SetInt32(SessionUserId, userId);
                    UserModel userDetail = new UserModel
                    {
                        userId = userId,
                        userFullName = fullName,
                        userEmail = email
                    };
                    string tokenString = this.manager.GenerateToken(userData.Email);
                    //logger.LogInformation(result + Environment.NewLine + DateTime.Now);
                    return this.Ok(new { Status = true, Message = result, Data = userDetail, Token = tokenString });
                }
                else
                {
                    //logger.LogWarning(result + Environment.NewLine + DateTime.Now);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                //logger.LogError(ex.Message + Environment.NewLine + DateTime.Now);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Check if reset password successful
        /// </summary>
        /// <param name="userData">UserLoginModel class</param>
        /// <returns>Http response</returns>
        [HttpPut]
        [Route("resetpassword")]
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
        [Route("forgotpassword")]
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

using BookStoreModels;
using Microsoft.Extensions.Configuration;

namespace BookStoreManager.Manager
{
    public interface IUserManager
    {
        IConfiguration Configuration { get; }

        string Register(UserModel userData);
        string Login(UserLoginModel userData);
        string GenerateToken(string email);
        string ResetPassword(UserLoginModel userData);
    }
}
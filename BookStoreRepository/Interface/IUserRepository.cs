using BookStoreModels;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public interface IUserRepository
    {
        string connectionString { get; set; }

        string EncryptPassword(string password);
        string Register(UserModel user);
        string Login(UserLoginModel user);
        string ResetPassword(UserLoginModel user);
        Task<string> ForgotPassword(string email);
    }
}
using BookStoreModels;

namespace BookStoreRepository.Repository
{
    public interface IUserRepository
    {
        string connectionString { get; set; }

        string EncryptPassword(string password);
        string Register(UserModel user);
        string Login(UserLoginModel user);
    }
}
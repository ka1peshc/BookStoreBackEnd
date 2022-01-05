using BookStoreModels;
using Microsoft.Extensions.Configuration;

namespace BookStoreManager.Manager
{
    public interface ICartManager
    {
        IConfiguration Configuration { get; }

        string AddNewItem(CartModel item);
    }
}
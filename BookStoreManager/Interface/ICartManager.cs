using BookStoreModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreManager.Manager
{
    public interface ICartManager
    {
        IConfiguration Configuration { get; }

        string AddNewItem(CartModel item);
        string DeleteItem(int itemId);
        string UpdateItemQuantity(int itemId, int count);
        IEnumerable<CartModel> DisplayCartItem(int userId);
    }
}
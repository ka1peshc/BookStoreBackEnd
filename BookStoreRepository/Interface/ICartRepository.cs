using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreRepository.Repository
{
    public interface ICartRepository
    {
        string connectionString { get; set; }

        string AddNewItemInCart(CartModel item);
        string DeleteItemFromCart(int itemId);
        string UpdateItemQuantity(int itemId, int count);
        IEnumerable<CartModel> GetItemsInCart(int userId);
    }
}
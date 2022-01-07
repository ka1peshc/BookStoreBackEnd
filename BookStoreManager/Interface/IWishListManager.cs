using BookStoreModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreManager.Manager
{
    public interface IWishListManager
    {
        IConfiguration Configuration { get; }

        string AddNewItemInWishList(WishListModel item);
        IEnumerable<WishListModel> DisplayCartItem(int userId);
        string DeleteItemInWishList(int wishListItemId);
    }
}
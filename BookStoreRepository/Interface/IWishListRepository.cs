using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreRepository.Repository
{
    public interface IWishListRepository
    {
        string connectionString { get; set; }

        string AddNewItemInWishList(WishListModel item);
        IEnumerable<WishListModel> FetchWishListOfUser(int userId);
        string DeleteItemInWishList(int wishListItemId);
    }
}
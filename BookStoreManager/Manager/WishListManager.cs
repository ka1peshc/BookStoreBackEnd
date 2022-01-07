using BookStoreModels;
using BookStoreRepository.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class WishListManager : IWishListManager
    {
        private readonly IWishListRepository repository;
        public WishListManager(IWishListRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public string AddNewItemInWishList(WishListModel item)
        {
            try
            {
                return this.repository.AddNewItemInWishList(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<WishListModel> DisplayCartItem(int userId)
        {
            try
            {
                return this.repository.FetchWishListOfUser(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string DeleteItemInWishList(int wishListItemId)
        {
            try
            {
                return this.repository.DeleteItemInWishList(wishListItemId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

using BookStoreModels;
using BookStoreRepository.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepository repository;
        public CartManager(ICartRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public string AddNewItem(CartModel item)
        {
            try
            {
                return this.repository.AddNewItemInCart(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string DeleteItem(int itemId)
        {
            try
            {
                return this.repository.DeleteItemFromCart(itemId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string UpdateItemQuantity(int itemId,int count)
        {
            try
            {
                return this.repository.UpdateItemQuantity(itemId,count);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<CartModel> DisplayCartItem(int userId)
        {
            try
            {
                return this.repository.GetItemsInCart(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

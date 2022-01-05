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
    }
}

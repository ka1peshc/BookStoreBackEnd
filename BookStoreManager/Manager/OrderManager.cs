using BookStoreModels;
using BookStoreRepository.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository repository;
        public OrderManager(IOrderRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public string AddNewOrder(OrderModel om)
        {
            try
            {
                return this.repository.AddNewOrder(om);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<OrderModel> DisplayOrderList(int userId)
        {
            try
            {
                return this.repository.DisplayOrderList(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<OrderModel> DisplaySuccessfulOrder(int orderId)
        {
            try
            {
                return this.repository.DisplaySuccessfulOrder(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

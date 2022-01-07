using BookStoreModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreManager.Manager
{
    public interface IOrderManager
    {
        IConfiguration Configuration { get; }

        string AddNewOrder(OrderModel om);
        IEnumerable<OrderModel> DisplayOrderList(int userId);
        IEnumerable<OrderModel> DisplaySuccessfulOrder(int orderId);
    }
}
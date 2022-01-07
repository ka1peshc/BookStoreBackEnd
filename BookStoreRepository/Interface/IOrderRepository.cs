using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreRepository.Repository
{
    public interface IOrderRepository
    {
        string connectionString { get; set; }

        string AddNewOrder(OrderModel om);
        IEnumerable<OrderModel> DisplayOrderList(int userId);
        IEnumerable<OrderModel> DisplaySuccessfulOrder(int orderId);
    }
}
using BookStoreModels;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public OrderRepository(IConfiguration configration)
        {
            this.config = configration;
        }

        public string AddNewOrder(OrderModel om)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_InsertOrder", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spUserId", om.UserId);
                    cmd.Parameters.AddWithValue("@spBookId", om.BookId);
                    cmd.Parameters.AddWithValue("@spAddressId", om.AddressId);
                    cmd.Parameters.AddWithValue("@spOrderDate", om.OrderDate);
                    cmd.Parameters.AddWithValue("@spNumBooks", om.BookQuantity);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Order added successful";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        public IEnumerable<OrderModel> DisplayOrderList(int userId)
        {
            try
            {
                List<OrderModel> tempList = new List<OrderModel>();
                IEnumerable<OrderModel> result;
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_DisplayOrderDetail", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spUserId", userId);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            OrderModel om = new OrderModel();
                            BookModel bm = new BookModel();
                            om.OrderId = Convert.ToInt32(rdr["orderId"]);
                            bm.bookId = Convert.ToInt32(rdr["bookId"]);
                            bm.bookName = rdr["bookName"].ToString();
                            bm.bookAuthor = rdr["bookAuthor"].ToString();
                            bm.bookDetail = rdr["bookDetail"].ToString();
                            bm.bookActualPrice = Convert.ToInt32(rdr["bookActualPrice"]);
                            bm.bookImageURL = rdr["bookImageURL"].ToString();
                            om.Price = Convert.ToInt32(rdr["price"]);
                            om.BookModel = bm;
                            tempList.Add(om);
                        }
                        return result = tempList;
                    }
                    return result = tempList;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        public IEnumerable<OrderModel> DisplaySuccessfulOrder(int orderId)
        {
            try
            {
                List<OrderModel> tempList = new List<OrderModel>();
                IEnumerable<OrderModel> result;
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_OrderedSucceful", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spOrderId", orderId);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            OrderModel om = new OrderModel();
                            UserModel um = new UserModel();
                            AddressModel am = new AddressModel();
                            
                            om.OrderId = Convert.ToInt32(rdr["orderId"]);
                            um.userPhoneNo = Convert.ToDouble(rdr["userPhoneNum"]);
                            am.Address = rdr["address"].ToString();
                            om.UserModel = um;
                            om.AddressModel = am;
                            tempList.Add(om);
                        }
                        return result = tempList;
                    }
                    return result = tempList;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}

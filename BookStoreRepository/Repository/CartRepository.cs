using BookStoreModels;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public CartRepository(IConfiguration configration)
        {
            this.config = configration;
        }

        public string AddNewItemInCart(CartModel item)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_CartAddItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spUserId", item.UserId);
                    cmd.Parameters.AddWithValue("@spBookId", item.BookId);
                    cmd.Parameters.AddWithValue("@spBookQuan", item.BookQuantity);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Item added to cart";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}

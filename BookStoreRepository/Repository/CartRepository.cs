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

        /// <summary>
        /// Delete item from cart
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public string DeleteItemFromCart(int itemId)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_CartDeleteItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spItemId", itemId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Item delete successful";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        public string UpdateItemQuantity(int itemId,int count)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_CartUpdateBookQuantity", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spItemId", itemId);
                    cmd.Parameters.AddWithValue("@spIncQuantity", count);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Item Update successful";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        public IEnumerable<CartModel> GetItemsInCart(int userId)
        {
            try
            {
                List<CartModel> tempList = new List<CartModel>();
                IEnumerable<CartModel> result;
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_CartDisplayAllItems", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spUserId",userId);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            CartModel cm = new CartModel();
                            BookModel bm = new BookModel();
                            cm.ItemId = Convert.ToInt32(rdr["itemId"]);
                            cm.BookId= Convert.ToInt32(rdr["bookId"]);
                            bm.bookName = rdr["bookName"].ToString();
                            bm.bookAuthor = rdr["bookAuthor"].ToString();
                            bm.bookDetail = rdr["bookDetail"].ToString();
                            bm.bookActualPrice = Convert.ToInt32(rdr["bookActualPrice"]);
                            bm.bookDiscountPrice = Convert.ToInt32(rdr["bookDiscountPrice"]);
                            bm.bookImageURL = rdr["bookImageURL"].ToString();
                            cm.BookQuantity = Convert.ToInt32(rdr["bookQuantity"]);
                            cm.BookModel = bm;
                            tempList.Add(cm);
                        }
                    }
                    result = tempList;
                    con.Close();
                    return result;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}

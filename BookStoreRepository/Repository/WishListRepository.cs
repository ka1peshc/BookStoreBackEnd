using BookStoreModels;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class WishListRepository : IWishListRepository
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public WishListRepository(IConfiguration configration)
        {
            this.config = configration;
        }

        public IEnumerable<WishListModel> FetchWishListOfUser(int userId)
        {
            try
            {
                List<WishListModel> tempList = new List<WishListModel>();
                IEnumerable<WishListModel> result;
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_DisplayWishlist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spUserId", userId);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            WishListModel am = new WishListModel();
                            BookModel bm = new BookModel();
                            am.WishListItemId = Convert.ToInt32(rdr["wlistItemId"]);
                            bm.bookName = rdr["bookName"].ToString();
                            bm.bookAuthor = rdr["bookAuthor"].ToString();
                            bm.bookActualPrice = Convert.ToInt32(rdr["bookActualPrice"]);
                            bm.bookDiscountPrice = Convert.ToInt32(rdr["bookDiscountPrice"]);
                            bm.bookImageURL = rdr["bookImageURL"].ToString();
                            am.BookId = Convert.ToInt32(rdr["bookId"]);
                            am.BookModel = bm;
                            tempList.Add(am);
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

        public string AddNewItemInWishList(WishListModel item)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_AddBookToWishList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spBookId", item.BookId);
                    cmd.Parameters.AddWithValue("@spUserId", item.UserId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Book added to wishlist";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
        public string DeleteItemInWishList(int wishListItemId)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_DeleteWishlistItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spWishListItem", wishListItemId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Book removed from wishlist";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}

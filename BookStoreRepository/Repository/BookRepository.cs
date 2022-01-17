using BookStoreModels;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public BookRepository(IConfiguration configration)
        {
            this.config = configration;
        }

        public string AddNewBook(BookModel model)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_AddNewBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spBookName", model.bookName);
                    cmd.Parameters.AddWithValue("@spBookAuthor", model.bookAuthor);
                    cmd.Parameters.AddWithValue("@spBookDetail", model.bookDetail);
                    cmd.Parameters.AddWithValue("@spBookActualPrise", model.bookActualPrice);
                    cmd.Parameters.AddWithValue("@spBookDiscountPrice", model.bookDiscountPrice);
                    cmd.Parameters.AddWithValue("@spBookImageUrl", model.bookImageURL);
                    cmd.Parameters.AddWithValue("@spBookQuantity", model.bookQuantity);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Book Added Successful";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }


        public string DeleteBook(int bookId)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_DeleteBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spBookId", bookId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Book Deleted Successful";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public string UpdateBook(BookModel model)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_UpdateBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spBookId", model.bookId);
                    cmd.Parameters.AddWithValue("@spBookName", model.bookName);
                    cmd.Parameters.AddWithValue("@spBookAuthor", model.bookAuthor);
                    cmd.Parameters.AddWithValue("@spBookDetail", model.bookDetail);
                    cmd.Parameters.AddWithValue("@spBookActualPrise", model.bookActualPrice);
                    cmd.Parameters.AddWithValue("@spBookDiscountPrice", model.bookDiscountPrice);
                    cmd.Parameters.AddWithValue("@spBookImageUrl", model.bookImageURL);
                    cmd.Parameters.AddWithValue("@spBookQuantity", model.bookQuantity);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Update Book Successful";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public IEnumerable<BookModel> DisplayOneBook(int bookId)
        {
            try
            {
                List<BookModel> tempList = new List<BookModel>();
                IEnumerable<BookModel> result;
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_DisplayOneBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spBookId", bookId);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    BookModel bm = new BookModel();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            bm.bookId = Convert.ToInt32(rdr["bookId"]);
                            bm.bookName = rdr["bookName"].ToString();
                            bm.bookAuthor = rdr["bookAuthor"].ToString();
                            bm.bookDetail = rdr["bookDetail"].ToString();
                            bm.bookActualPrice = Convert.ToInt32(rdr["bookActualPrice"]);
                            bm.bookDiscountPrice = Convert.ToInt32(rdr["bookDiscountPrice"]);
                            bm.bookQuantity = Convert.ToInt32(rdr["bookQuantity"]);
                            bm.bookImageURL = rdr["bookImageURL"].ToString();
                            bm.avgRating = Convert.ToInt32(rdr["bookRating"]);
                            bm.countRating = Convert.ToInt32(rdr["bookRatingCount"]);
                            
                        }
                    }
                    tempList.Add(bm);
                    result = tempList;
                    return result;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        public IEnumerable<BookModel> DisplayAllBooks()
        {
            try
            {
                List<BookModel> tempList = new List<BookModel>();
                IEnumerable<BookModel> result;
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_DisplayAllBooks", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            BookModel bm = new BookModel();
                            bm.bookId = Convert.ToInt32(rdr["bookId"]);
                            bm.bookName = rdr["bookName"].ToString();
                            bm.bookAuthor = rdr["bookAuthor"].ToString();
                            bm.bookDetail = rdr["bookDetail"].ToString();
                            bm.bookActualPrice = Convert.ToInt32(rdr["bookActualPrice"]);
                            bm.bookDiscountPrice = Convert.ToInt32(rdr["bookDiscountPrice"]);
                            bm.bookQuantity = Convert.ToInt32(rdr["bookQuantity"]);
                            bm.bookImageURL = rdr["bookImageURL"].ToString();
                            bm.avgRating = Convert.ToInt32(rdr["bookRating"]);
                            bm.countRating = Convert.ToInt32(rdr["bookRatingCount"]);
                            tempList.Add(bm);
                        }
                    }
                    result = tempList;
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

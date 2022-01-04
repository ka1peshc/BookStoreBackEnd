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
    }
}

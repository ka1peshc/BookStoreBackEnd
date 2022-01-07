using BookStoreModels;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public FeedbackRepository(IConfiguration configration)
        {
            this.config = configration;
        }

        public string AddNewReview(FeedbackModel fm)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_InsertOrder", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spUserId", fm.UserId);
                    cmd.Parameters.AddWithValue("@spBookId", fm.BookId);
                    cmd.Parameters.AddWithValue("@spRating", fm.Rating);
                    cmd.Parameters.AddWithValue("@spReview", fm.Review);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "R added successful";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        public IEnumerable<FeedbackModel> DisplayReviewList(int bookId)
        {
            try
            {
                List<FeedbackModel> tempList = new List<FeedbackModel>();
                IEnumerable<FeedbackModel> result;
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_GetReview", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spBookId", bookId);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            FeedbackModel fm = new FeedbackModel();
                            UserModel um = new UserModel();
                            fm.FeedbackId = Convert.ToInt32(rdr["feedbackId"]);
                            um.userFullName = rdr["userFullName"].ToString();
                            fm.Rating = Convert.ToInt32(rdr["rating"]);
                            fm.Review = rdr["review"].ToString();
                            fm.UserModel = um;
                            tempList.Add(fm);
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

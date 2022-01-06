using BookStoreModels;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public AddressRepository(IConfiguration configration)
        {
            this.config = configration;
        }

        public IEnumerable<AddressModel> FetchAddressOfUser(int userId)
        {
            try
            {
                List<AddressModel> tempList = new List<AddressModel>();
                IEnumerable<AddressModel> result;
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_DisplayAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spUserId", userId);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            AddressModel am = new AddressModel();
                            am.AddressId = Convert.ToInt32(rdr["addressId"]);
                            am.Address = rdr["address"].ToString();
                            am.Town = rdr["town"].ToString();
                            am.State = rdr["state"].ToString();
                            am.TypeId = Convert.ToInt32(rdr["addressType"]);
                            tempList.Add(am);
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

        public string UpdateAddress(AddressModel addModel)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_UpdateAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spAddressId", addModel.AddressId);
                    cmd.Parameters.AddWithValue("@spAddress", addModel.Address);
                    cmd.Parameters.AddWithValue("@spTown", addModel.Town);
                    cmd.Parameters.AddWithValue("@spState", addModel.State);
                    cmd.Parameters.AddWithValue("@spAddressType", addModel.TypeId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Update address successful";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        public string InsertAddress(AddressModel addModel)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_AddNewAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@spUserId", addModel.UserId);
                    cmd.Parameters.AddWithValue("@spAddress", addModel.Address);
                    cmd.Parameters.AddWithValue("@spTown", addModel.Town);
                    cmd.Parameters.AddWithValue("@spState", addModel.State);
                    cmd.Parameters.AddWithValue("@spAddressType", addModel.TypeId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Insert address successful";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BookStoreModels;
using MySql.Data.MySqlClient;
using System.Data;
using StackExchange.Redis;

namespace BookStoreRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public UserRepository(IConfiguration configration)
        {
            this.config = configration;
        }

        /// <summary>
        /// Register New user and check if email already exist
        /// </summary>
        /// <param name="user">UserModel class</param>
        /// <returns>string message</returns>
        public string Register(UserModel user)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                int result = 0;
                string msg = string.Empty;
                string protectedPassword = EncryptPassword(user.userPassword);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_AddUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@firstName", user.userFirstName);
                    cmd.Parameters.AddWithValue("@lastName", user.userLastName);
                    cmd.Parameters.AddWithValue("@email", user.userEmail);
                    cmd.Parameters.AddWithValue("@userPass", protectedPassword);
                    cmd.Parameters.AddWithValue("@userPhone", user.userPhoneNo);
                    con.Open();
                    //cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    //Switch statement
                    msg = result switch
                    {
                        -1 => "Email Already Exist",
                        _ => "Registration Successful",
                    };
                }
                return msg;
                
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
        /// <summary>
        /// Authanticate user email and password
        /// </summary>
        /// <param name="user">UserLoginModel class</param>
        /// <returns>string message</returns>
        public string Login(UserLoginModel user)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_LoginUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@userPass", EncryptPassword(user.Password));
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    UserModel userDetail = new UserModel();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            userDetail.userId = Convert.ToInt32(rdr["userId"]);
                            userDetail.userFirstName = rdr["userFirstName"].ToString();
                            userDetail.userLastName = rdr["userLastName"].ToString();
                            userDetail.userEmail = rdr["userEmail"].ToString();
                        }
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                        IDatabase database = connectionMultiplexer.GetDatabase();
                        database.StringSet(key: "First Name", userDetail.userFirstName);
                        database.StringSet(key: "Last Name", userDetail.userLastName);
                        database.StringSet(key: "email", userDetail.userEmail);
                        database.StringSet(key: "User Id", userDetail.userId);
                        con.Close();
                        return "Login Successful";
                    }
                    else
                    {
                        return "Login Unsuccessful";
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        public string ResetPassword(UserLoginModel user)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_UpdateUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@newPassword", EncryptPassword(user.Password));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Password Update Successful";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
        public string EncryptPassword(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            ////encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            ////Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }
        
    }
}

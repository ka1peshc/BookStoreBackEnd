using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModels
{
    public class UserModel
    {
        [Key]
        [Required]
        public int userId { get; set; }
        [Required]
        public string userFullName { get; set; }
        [Required]
        public string userEmail { get; set; }
        [Required]
        public string userPassword { get; set; }
        [Required]
        public double userPhoneNo { get; set; }

    }
}

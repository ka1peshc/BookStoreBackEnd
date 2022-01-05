using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreModels
{
    public class CartModel
    {
        [Key]
        [Required]
        public int ItemId { get; set; }
        [ForeignKey("UserModel")]
        public int UserId { get; set; }
        public virtual UserModel UserModel { get; set; }
        [ForeignKey("BookModel")]
        public int BookId { get; set; }
        public virtual BookModel BookModel { get; set; }
        [DefaultValue(1)]
        public int BookQuantity { get; set; }

    }
}

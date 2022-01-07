using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreModels
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("BookId")]
        public int BookId { get; set; }
        public virtual BookModel BookModel { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual UserModel UserModel { get; set; }
        [ForeignKey("AddressId")]
        public int AddressId { get; set; }
        public virtual AddressModel AddressModel { get; set; }
        public DateTime OrderDate { get; set; }
        public int Price { get; set; }
        public int BookQuantity { get; set; }
    }
}

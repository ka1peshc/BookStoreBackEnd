using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreModels
{
    public class WishListModel
    {
        [Key]
        public int WishListItemId { get; set; }
        [ForeignKey("BookModel")]
        public int BookId { get; set; }
        public virtual BookModel BookModel { get; set; }
        public int UserId { get; set; }
    }
}

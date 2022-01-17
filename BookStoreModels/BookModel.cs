using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModels
{
    public class BookModel
    {
        [Key]
        [Required]
        public int bookId { get; set; }
        [Required]
        public string  bookName { get; set; }
        [Required]
        public string bookAuthor { get; set; }
        [Required]
        public string bookDetail { get; set; }
        [Required]
        public int bookActualPrice { get; set; }
        [Required]
        public int bookDiscountPrice { get; set; }
        [Required]
        public string bookImageURL { get; set; }
        [Required]
        public int bookQuantity { get; set; }
        public float avgRating { get; set; }
        public int countRating { get; set; }
    }
}

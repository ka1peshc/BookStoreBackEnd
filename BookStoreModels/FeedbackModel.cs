using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreModels
{
    public class FeedbackModel
    {
        [Key]
        public int FeedbackId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual UserModel UserModel { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionShop.Models
{
    public class FeedbackModel
    {
        [Required, Display(Name = "Nhập họ tên!")]
        public string FeedbackName { get; set; }
        [Required, Display(Name = "Nhập E-mail"), EmailAddress]
        public string Email { get; set; }
        [Required, Display(Name = "Tiêu đề")]
        public string Subject { get; set; }
        [Required, Display(Name = "Nhập phản hồi!")]
        public string Message { get; set; }
    }
}
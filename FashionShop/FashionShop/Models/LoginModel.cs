using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models
{
    public class LoginModel
    {
        [Required (ErrorMessage = "Bạn phải nhập email hoặc số điện thoại!")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Bạn phải nhập mật khẩu!")]
        public string Password { get; set; }
    }
}
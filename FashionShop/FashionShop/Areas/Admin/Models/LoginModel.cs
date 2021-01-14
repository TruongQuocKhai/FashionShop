using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Display(Name = "Email đăng nhập")]
        [Required(ErrorMessage = "Bạn phải nhập email!")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Nhập mật khẩu!")]
        public string Password { get; set; }

       // public bool RememberMe { get; set; }
    }
}
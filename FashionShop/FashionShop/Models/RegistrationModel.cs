using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; // chu thich du lieu

namespace FashionShop.Models
{
    public class RegistrationModel
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Vui lòng nhập họ và tên!")]
        public string DisplayName { get; set; }

        [Required (ErrorMessage = "Vui lòng nhập số điện thoại!")]
        public string Phone { get; set; }

        [Required (ErrorMessage = "Vui lòng nhập email!")]
        public string Email { get; set; }

        [StringLength (20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu it nhất 6 ký tự!")]
        [Required (ErrorMessage = "Vui lòng nhập mật khẩu!")]
        public string Password { get; set; }

        //[Display(Name = "Xác nhận mật khẩu")]
        //[Compare("Password", ErrorMessage = "Xác nhận mật khẩu chưa đúng!")]
        //public string ConfirmPassword { get; set; }
    }
}
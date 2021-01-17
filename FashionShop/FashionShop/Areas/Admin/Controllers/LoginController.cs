using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Common;
using FashionShop.Models;
using Model.ADO;

namespace FashionShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userAdo = new UserADO();
                var result = userAdo.Login(model.Email, Encryption.MD5Hash(model.Password), true);
                if (result == 1)
                {
                    var user = userAdo.GetEmail(model.Email);
                    var userProfile = new UserProfile();
                    userProfile.UserId = user.user_id;
                    userProfile.UserEmail = user.email;
                    userProfile.GivenName = user.display_name;
                    Session.Add(SessionConst.USER_SESSION, userProfile);
                    return Redirect("/Admin/dashboard");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa!");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng!");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập!");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng!");
                }
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session[SessionConst.USER_SESSION] = null;
            return Redirect("/Admin");
        }
    }
}
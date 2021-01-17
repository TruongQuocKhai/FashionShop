using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models;
using FashionShop.Common;
using Model.ADO;
using Model.EF;

namespace FashionShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 3)
        {
            int totalRecord = 0;
            var model = new UserADO().GetListUser(ref totalRecord, page, pageSize);
            // Tổng bản ghi lấy đc trong database / số lượng user muốn hiển thị trên màn hình.
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.DisplayMaxPages = 6;
            ViewBag.StartIndex = page;
            ViewBag.Next = page + 1;
            ViewBag.Pre = page - 1;
            return View(model);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(user userOfParameterPassed)
        {
            if (ModelState.IsValid)
            {
                var userAdo = new UserADO();
                if (userAdo.CheckEmail(userOfParameterPassed.email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại!");
                }
                else if (userAdo.CheckPhone(userOfParameterPassed.phone))
                {
                    ModelState.AddModelError("", "Số điện thoại đã tồn tại!");
                }
                else
                {
                    //var encryptedMD5Pas = Encryption.MD5Hash(userEntity.password);
                    var dbUser = new user();
                    dbUser.user_group_id = userOfParameterPassed.user_group_id;
                    dbUser.password = Encryption.MD5Hash(userOfParameterPassed.password);
                    dbUser.display_name = userOfParameterPassed.display_name;
                    dbUser.email = userOfParameterPassed.email;
                    dbUser.phone = userOfParameterPassed.phone;
                    dbUser.created_date = DateTime.Now;
                    dbUser.status = true;
                    var userId = userAdo.Insert(dbUser);
                    if (userId > 0)
                    {
                        SetAlert("Thêm mới user thành công!", "success");
                        return Redirect("/Admin/quan-ly-user");
                    }
                    else
                    {
                        SetAlert("Thêm mới user thất bại!", "danger");
                    }
                }
            }
            return View(userOfParameterPassed);
        }

        public JsonResult DeleteUser(int id)
        {
            try
            {
                var model = new UserADO();
                model.Delete(id);
                return Json(new
                {
                    message = 1
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    message = 0
                });
            }
        }
    }
}
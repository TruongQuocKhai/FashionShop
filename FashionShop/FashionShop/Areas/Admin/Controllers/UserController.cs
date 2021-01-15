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
        public ActionResult AddUser(user userEntity)
        {
            if (ModelState.IsValid)
            {
                var userAdo = new UserADO();
                if (userAdo.CheckEmail(userEntity.email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại!");
                }
                else if (userAdo.CheckPhone(userEntity.phone))
                {
                    ModelState.AddModelError("", "Số điện thoại đã tồn tại!");
                }
                else
                {
                    //var encryptedMD5Pas = Encryption.MD5Hash(userEntity.password);
                    var userId = userAdo.Insert(userEntity);
                    if (userId > 0)
                    {
                        return Redirect("/Admin/quan-ly-user");
                    }
                    return View("Index"); 
                }
            }
            return View("Index");
        }

    }
}
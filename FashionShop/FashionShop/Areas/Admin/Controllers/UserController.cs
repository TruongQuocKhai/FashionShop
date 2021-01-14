using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ADO;

namespace FashionShop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 3)
        {
            int totalRecord = 0;
            var model = new UserADO().GetListUser(ref totalRecord, page, pageSize);

            // Tổng bản ghi lấy đc trong database / số lượng user muốn hiển thị trên màn hình.
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord/pageSize));

            ViewBag.TotalPage = totalPage;
            ViewBag.DisplayMaxPages = 6;
            ViewBag.StartIndex = page;
            ViewBag.Next = page + 1;
            ViewBag.Pre = page - 1;

            return View(model);
        }
    }
}
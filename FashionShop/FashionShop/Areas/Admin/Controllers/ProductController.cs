using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ADO;

namespace FashionShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(int page = 1, int pageSize = 3)
        {
            int totalRecord = 0;
            var model = new ProductADO().GetAllProducts(ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.StartIndex = page;

            int displayMaxPages = 6;
            int totalPage = 0;
            // Tổng bản ghi Lấy từ DB / số sp muốn hiển thị 
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));

            ViewBag.TotalPage = totalPage;
            ViewBag.DisplayMaxPages = displayMaxPages;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }

        public ActionResult AddProduct()
        {

        }
    }                      
}
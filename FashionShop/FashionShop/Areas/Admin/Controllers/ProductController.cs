using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ADO;
using Model.EF;

namespace FashionShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(int page = 1, int pageSize = 3)
        {
            int totalRecord = 0;
            var model = new ProductADO().GetAllProducts(ref totalRecord, page, pageSize);
           
            // Tổng bản ghi Lấy từ DB / số sp muốn hiển thị 
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));

            int displayMaxPages = 6;
            ViewBag.DisplayMaxPages = displayMaxPages;
            ViewBag.StartIndex = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }

        public void GetDropDownListCategory(int? selectedId = null)
        {
            var model = new ProductCategoryADO();
            ViewBag.CategoryId = new SelectList(model.GetListProductCategory(), "prd_category_id", "prd_category_name", selectedId);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            GetDropDownListCategory();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(product entity)
        {
            if (ModelState.IsValid)
            {
                new ProductADO().Insert(entity);
                return Redirect("Admin/quan-ly-san-pham");
            }
            GetDropDownListCategory();
            return View();
        }
    }                      
}
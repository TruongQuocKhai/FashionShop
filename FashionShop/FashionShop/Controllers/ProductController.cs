using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ADO;

namespace FashionShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult _ListNewProductsPartial()
        {
            var model = new ProductADO().GetListNewProducts(4);
            return PartialView(model);
        }

        public ActionResult _ListDiscountProductsPartial()
        {
            var model = new ProductADO().GetListDiscountProducts(8);
            return PartialView(model);
        }

        // totalItems (required) - the total number of items to be paged
        // currentPage (optional) - the current active page, default to first page
        // pageSize (optional) - the number of items per page, defaults to 10
        // maxPages (optional) - the maximum number of page navigation link to display, defaul to 7
        public ActionResult ListAllProducts(int page = 1, int pageSize = 4)// pageSize = productsPerPage
        {
            int totalRecord = 0;
            var model = new ProductADO().GetAllProducts(ref totalRecord, page, pageSize);

            ViewBag.TotalRecord = totalRecord;
            ViewBag.StartIndex = page;

            int totalPage = 0;
            int displayMaxPages = 6;

            // Tổng trang của số bản ghi / số lượng sp hiển thị 
            totalPage = (int)Math.Ceiling((double)totalRecord / pageSize); // Math.Ceiling(double a) -> ham lam tron so len
            ViewBag.TotalPage = totalPage;
            ViewBag.DisplayMaxPages = displayMaxPages;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

        public ActionResult ListProductsByCategory(int id)
        {
            var model = new ProductADO().GetListProductsByCategoryId(id);
            return View(model);
        }

        public ActionResult ViewProductDetails(int id)
        {
            var model = new ProductADO().GetProductId(id);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult _ListRelatedProductsPartial(int id)
        {
            var model = new ProductADO().GetListRelatedProducts(id);
            return PartialView(model);
        }

        public JsonResult ListName(string q)
        {
             var productName = new ProductADO().GetProductName(q);
            return Json(new
            {
                data = productName,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string keyword, int page = 1, int pageSize = 1)// pageSize = productsPerPage
        {
            int totalRecord = 0;
            var model = new ProductADO().GetSearchList(keyword, ref totalRecord, page, pageSize);

            ViewBag.TotalRecord = totalRecord;
            ViewBag.StartIndex = page;
            ViewBag.Keyword = keyword;

            int totalPage = 0;
            int displayMaxPages = 6;

            // Tổng trang của số bản ghi / số lượng sp hiển thị 
            totalPage = (int)Math.Ceiling((double)totalRecord / pageSize); // Math.Ceiling(double a) -> ham lam tron so len
            ViewBag.TotalPage = totalPage;
            ViewBag.DisplayMaxPages = displayMaxPages;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
    }
}
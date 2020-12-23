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

        public ActionResult ListAllProducts(int startIndex = 1, int pageSize = 4)
        {
            int totalRecord = 0;
            var model = new ProductADO().GetAllProducts(ref totalRecord, startIndex, pageSize);

            ViewBag.Page = startIndex;
            ViewBag.Total = totalRecord;

            int maxPage = 10;
            int perPage = 0;
            perPage = (int)Math.Ceiling((double)(totalRecord / pageSize));   // 8sp / 4trang -> mỗi trang 2sp

            ViewBag.PerPage = perPage;
            ViewBag.MaxPag = maxPage;
            ViewBag.FirstPage = 1;
            ViewBag.LastPage = 


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
    }
}
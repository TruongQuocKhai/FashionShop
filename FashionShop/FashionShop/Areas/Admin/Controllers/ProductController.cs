using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ADO;
using Model.EF;

namespace FashionShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(int page = 1, int pageSize = 3)
        {
            int totalRecord = 0;
            var model = new ProductADO().GetAllProductsForAdmin(ref totalRecord, page, pageSize);

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

        public void DropDownListCategory(int? selectedId = null)
        {
            var model = new ProductCategoryADO();
            ViewBag.CategoryId = new SelectList(model.GetListProductCategory(), "prd_category_id", "prd_category_name", selectedId);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            DropDownListCategory();
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddProduct(product entity)
        {
            if (ModelState.IsValid)
            {
                entity.created_date = DateTime.Now;
                entity.status = true;
                var result = new ProductADO().Insert(entity);
                if (result > 0)
                {
                    SetAlert("Thêm mới thành công!", "success");
                    return Redirect("/Admin/quan-ly-san-pham");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công!");
                }
            }
            DropDownListCategory();
            return View();
        }

        public JsonResult RemoveProduct(int id)
        {
            try
            {
                new ProductADO().Remove(id);
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

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            DropDownListCategory();
            var model = new ProductADO().GetProductId(id);
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditProduct(product prdEntity)
        {
            if (ModelState.IsValid)
            {
                prdEntity.created_date = DateTime.Now;
                var result = new ProductADO().Update(prdEntity);
                if (result)
                {
                    SetAlert("Cập nhật sản phẩm thành công!", "success");
                    return Redirect("/Admin/quan-ly-san-pham");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công!");
                }
            }
            DropDownListCategory();
            return View();
            // return Redirect("/Admin/quan-ly-san-pham");
        }
    }
}
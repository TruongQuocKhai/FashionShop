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
        // Ghi comment vào đầu hàm theo format dưới đây: 
        // Ghi rõ parameter được dùng cho (i)input hay (o)output hay sử dụng cho cả 2 (i/o)input và output
        /****************************************************************/
        /* Function Name : GetStatus()                                  */
        /* Function      : 状態を取得する                               */
        /* Param         : nStateID(i) 状態を取得する部品のID(0～15)    */
        /* Return        : ０ ： 正常な状態                             */
        /*               : １ ： エラー有り                             */
        /* Create        : S.Sato 1999/09/09                            */
        /* Update        :                                              */
        /* Comment       :                                              */
        /****************************************************************/
        public ActionResult _ListNewProductsPartial()
        {
            var model = new ProductADO().GetListNewProducts(8);
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
        public ActionResult ListAllProducts(int nPage = 1, int nPageSize = 4)// pageSize = productsPerPage
        {
            int nTotalRecord = 0;
            var model = new ProductADO().GetAllProducts(ref nTotalRecord, nPage, nPageSize);

            ViewBag.TotalRecord = nTotalRecord;
            ViewBag.StartIndex = nPage;

            int totalPage = 0;
            int displayMaxPages = 6;

            // Tổng trang của số bản ghi / số lượng sp hiển thị 
            totalPage = (int)Math.Ceiling((double)nTotalRecord / nPageSize); // Math.Ceiling(double a) -> ham lam tron so len
            ViewBag.TotalPage = totalPage;
            ViewBag.DisplayMaxPages = displayMaxPages;
            ViewBag.Next = nPage + 1;
            ViewBag.Prev = nPage - 1;

            return View(model);
        }

        /****************************************************************/
        /* Function Name : ListProductsByCategory()                   
        /* Function      : List of products by category               
        /* Param         : nCategoryId(i) category id to return the product list                                
        /* Return        : model ： return a product objet                 
        /* Create        : T.Khai 2021/01/11                            
        /* Update        :                                              
        /* Comment       :  
        /****************************************************************/
        public ActionResult ListProductsByCategory(int nCategoryId)
        {
            var model = new ProductADO().GetListProductsByCategoryId(nCategoryId);
            return View(model);
        }

        /****************************************************************/
        /* Function Name : ViewProductDetails()                   
        /* Function      : View product details               
        /* Param         : nPrdId(i) Product id to return                                 
        /* Return        : model ： return a record (1 hàng)                  
        /* Create        : T.Khai 2020/12/23                            
        /* Update        :                                              
        /* Comment       :  
        /****************************************************************/
        public ActionResult ViewProductDetails(int nPrdId)
        {
            var model = new ProductADO().GetProductId(nPrdId);
            return View(model);
        }

        /****************************************************************/
        /* Function Name : _ListRelatedProductsPartial()                   
        /* Function      : List of related products              
        /* Param         : nPrdId(i) Product id                                
        /* Return        : listRecords ： return list of records                  
        /* Create        : T.Khai 2020/12/23                            
        /* Update        :                                              
        /* Comment       :  
        /****************************************************************/
        [ChildActionOnly]
        public ActionResult _ListRelatedProductsPartial(int nPrdId)
        {
            var listRecords = new ProductADO().GetListRelatedProducts(nPrdId);
            return PartialView(listRecords);
        }

        /****************************************************************/
        /* Function Name   : ListProductName()                   
        /* Function Content: List product name              
        /* Param           : sKeyword(i) string key words                                 
        /* Return          : Json ： return list product name in Json Jquery format                
        /* Create          : T.Khai 2020/12/23                            
        /* Update          :                                              
        /* Comment         : Fife jquery: /Contents/js/JsFileController/search-autocomplete  
        /****************************************************************/
        public JsonResult ListProductName(string sKeyword)
        {
            var productName = new ProductADO().GetProductName(sKeyword);
            return Json(new
            {
                data = productName,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        /****************************************************************/
        /* Function Name   : Search()                   
        /* Function Content:              
        /* Param           : sKeyword(i), nPage(i), nPageSize(i)                                  
        /* Return          : Model: list all product            
        /* Create          : T.Khai 2021/01/11                            
        /* Update          : T.Khai 2021/01/20
         *                   Fix bug css
        /* Comment         :
        /****************************************************************/
        public ActionResult Search(string sKeyword, int nPage = 1, int nPageSize = 1)// pageSize = productsPerPage
        {
            int nTotalRecord = 0;
            int totalPage = 0;
            int displayMaxPages = 6;
            var model = new ProductADO().GetSearchList(sKeyword, ref nTotalRecord, nPage, nPageSize);
            ViewBag.TotalRecord = nTotalRecord;
            ViewBag.StartIndex = nPage;
            ViewBag.Keyword = sKeyword;

            // Tổng trang của số bản ghi / số lượng sp hiển thị 
            totalPage = (int)Math.Ceiling((double)nTotalRecord / nPageSize); // Math.Ceiling(double a) -> ham lam tron so len
            ViewBag.TotalPage = totalPage;
            ViewBag.DisplayMaxPages = displayMaxPages;
            ViewBag.Next = nPage + 1;
            ViewBag.Prev = nPage - 1;

            return View(model);
        }
    }
}
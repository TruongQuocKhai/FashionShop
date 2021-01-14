using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionShop.Models;
using Model.EF;

namespace Model.ADO
{
    public class ProductADO
    {
        DbFashionShop db = null;
        public ProductADO()
        {
            db = new DbFashionShop();
        }


        public int Insert(product entity)
        {
            db.product.Add(entity);
            db.SaveChanges();
            return entity.product_id;
        }

        public List<product> GetListNewProducts(int quantity)
        {
            return db.product.Where(x => x.discount == null).OrderByDescending(x => x.created_date).Take(quantity).ToList();
        }

        public List<product> GetListDiscountProducts(int quantity)
        {
            return db.product.Where(x => x.discount != null).OrderByDescending(x => x.created_date).Take(quantity).ToList();
        }

        // Pagination
        public List<product> GetAllProducts(ref int totalRecord, int page, int pageSize)
        {
            totalRecord = db.product.Count(); // get total record of products
            return db.product.OrderByDescending(x => x.created_date).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        //public List<ProductAndCategoryJoinModel> GetSearchList(string keyword, ref int totalRecord, int page, int pageSize)
        //{
        //    totalRecord = db.product.Where(x => x.product_name == keyword).Count();
        //    var model = (from p in db.product
        //                 join c in db.product_category
        //                 on p.product_id equals c.prd_category_id
        //                 where p.product_name.Contains(keyword)
        //                 select new
        //                 {
        //                     CateName = c.prd_category_name,  // Tên danh mục
        //                     CateAlias = c.alias,             // alias danh mục
        //                     ProdId = p.product_id,               // id sản phẩm
        //                     ProdName = p.product_name,           // tên sp
        //                     ProdAlias = p.alias,             // alias sp
        //                     ProdImages = p.image,                // ảnh sp
        //                     ProdPrice = p.price,                 // giá sp
        //                     CreatedDate = p.created_date,    // ngày tạo sp
        //                 }).AsEnumerable().Select(x => new ProductAndCategoryJoinModel()
        //                 {
        //                     CateName = x.CateName,  // Tên danh mục
        //                     CateAlias = x.CateAlias,             // alias danh mục
        //                     ProdId = x.ProdId,               // id sản phẩm
        //                     ProdName = x.ProdName,           // tên sp
        //                     ProdAlias = x.ProdAlias,             // alias sp
        //                     ProdImages = x.ProdImages,                // ảnh sp
        //                     ProdPrice = x.ProdPrice,                 // giá sp
        //                     CreatedDate = x.CreatedDate,
        //                 });
        //    return model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        //}

        public List<product> GetSearchList(string keyword, ref int totalRecord, int page, int pageSize )
        {
            totalRecord = db.product.Where(x => x.product_name.Contains(keyword)).Count();
            return db.product.Where(x => x.product_name == keyword).OrderByDescending(x => x.created_date).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<product> GetListProductsByCategoryId(int id)
        {
            return db.product.Where(x => x.prd_category_id == id).OrderByDescending(x => x.created_date).ToList();
        }

        public product GetProductId(int id)
        {
            return db.product.Find(id);
        }

        public List<product> GetListRelatedProducts(int id)
        {
            var product = db.product.Find(id);
            return db.product.Where(x => x.product_id != id && x.prd_category_id == product.prd_category_id).ToList();
        }

        public List<string> GetProductName(string keyword)
        {
            return db.product.Where(x => x.product_name.Contains(keyword)).Select(x => x.product_name).ToList();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<product> GetListNewProducts(int quantity)
        {
            return db.product.OrderByDescending(x => x.created_date).Take(quantity).ToList();
        }

        public List<product> GetListDiscountProducts(int quantity)
        {
            return db.product.Where(x => x.discount != null).OrderByDescending(x => x.created_date).Take(quantity).ToList();
        }

        public List<product> GetAllProducts(ref int totalRecord, int startIndex, int pageSize)
        {
            totalRecord = db.product.Count(); // Dem so dong (record)
            return db.product.OrderByDescending(x => x.created_date).Skip(startIndex).Take(pageSize).ToList();
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
    }
}

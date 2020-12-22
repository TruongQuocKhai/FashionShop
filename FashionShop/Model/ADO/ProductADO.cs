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

        public List<product> GetListNewProduct(int quantity)
        {
            return db.product.OrderByDescending(x => x.created_date).Take(quantity).ToList();
        }

        public List<product> GetListDiscountProducts(int quantity)
        {
            return db.product.Where(x => x.top_hot != null && x.top_hot > DateTime.Now).OrderByDescending(x => x.discount != null).Take(quantity).ToList();
        }
    }
}

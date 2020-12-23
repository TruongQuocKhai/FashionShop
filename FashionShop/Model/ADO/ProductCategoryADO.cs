using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.ADO
{
    public class ProductCategoryADO
    {
        DbFashionShop db = null;
        public ProductCategoryADO()
        {
            db = new DbFashionShop();
        }

        public List<product_category> GetListProductCategory()
        {
            return db.product_category.Where(x => x.status == true).OrderBy(x => x.display_order).ToList();
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.ADO
{
    public class NewsCategoryADO
    {
        DbFashionShop db = null;
        public NewsCategoryADO()
        {
            db = new DbFashionShop();
        }

        public List<news_category> GetListNewsCategory()
        {
            return db.news_category.ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.ADO
{
    public class SlideADO
    {
        DbFashionShop db = null;
        public SlideADO()
        {
            db = new DbFashionShop();
        }

        public List<slide> GetListSlide()
        {
            return db.slide.Where(x => x.status == true).OrderBy(x => x.display_order).ToList(); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.ADO
{
    public class FooterADO
    {
        DbFashionShop db = null; 
        public FooterADO()
        {
            db = new DbFashionShop();
        }

        public footer GetFooter()
        {
            // SingleOrderDefault: returns only a single record
            return db.footer.SingleOrDefault(x => x.status == true);
        }
    }
}

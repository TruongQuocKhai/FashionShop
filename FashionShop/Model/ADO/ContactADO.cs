using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.ADO
{
    public class ContactADO
    {
        DbFashionShop db = null;
        public ContactADO()
        {
            db = new DbFashionShop();
        }

        public contact GetInfoContact()
        {
            return db.contact.Single(x => x.status == true);
        }
    }
}

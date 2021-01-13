using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.ADO
{
    public class IntroduceADO
    {
        DbFashionShop db = null;
        public IntroduceADO()
        {
            db = new DbFashionShop();
        }

        public introduce GetIntroduceInfo()
        {
            return db.introduce.SingleOrDefault();
        }
    }
}

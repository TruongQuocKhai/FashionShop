using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.ADO
{
    public class ContentADO
    {
        DbFashionShop db = null;
        public ContentADO()
        {
            db = new DbFashionShop();
        }

        public List<content> GetInforContent()
        {
            return db.content.ToList();
        }

        public content GetContentId(int id)
        {
            return db.content.Find(id);
        }
    }
}

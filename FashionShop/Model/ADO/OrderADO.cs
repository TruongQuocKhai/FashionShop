using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.ADO
{
    public class OrderADO
    {
        DbFashionShop db = null;
        public OrderADO()
        {
            db = new DbFashionShop();
        }

        public int Insert(order order)
        {
            db.order.Add(order);
            db.SaveChanges();
            return order.order_id;
        }

    }
}

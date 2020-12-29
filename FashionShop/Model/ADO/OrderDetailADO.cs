using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.ADO
{
    public class OrderDetailADO
    {
        DbFashionShop db = null;
        public OrderDetailADO()
        {
            db = new DbFashionShop();
        }

        public bool Insert(order_detail orderDetail)
        {
            try
            {
                db.order_detail.Add(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        //public order_detail InsertData()
        //{
        //    var order = new order_detail();
        //    order
        //}
    }
}

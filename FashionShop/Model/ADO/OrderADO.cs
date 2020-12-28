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

        //public order GetOrderInfo(string name, string email, string phone, string address)
        //{
        //    var order = new order();
        //    order.order_name = name;
        //    order.order_email = email;
        //    order.order_phone = phone;
        //    order.order_address = address;
        //    return order;
        //}

    }
}

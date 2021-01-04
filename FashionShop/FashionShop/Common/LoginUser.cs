using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Common
{
    public class LoginUser
    {
        public long UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string GroupID { get; set; }
    }
}
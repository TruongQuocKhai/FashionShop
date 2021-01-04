using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.ADO
{
    public class UserADO
    {
        DbFashionShop db = null;
        public UserADO()
        {
            db = new DbFashionShop();
        }

        public bool CheckEmail(string email)
        {
            return db.user.Count(x => x.email == email) > 0;
        }

        public int Insert(user entity)
        {
            db.user.Add(entity);
            db.SaveChanges();
            return entity.user_id;
        }

        public int Login(string email, string password, bool hasLoggedAdmin = false)
        {
            var result = db.user.SingleOrDefault(x => x.email == email);
            if (result == null)
            {
                return 0; // Account already exits
            }
            else
            {
                if (hasLoggedAdmin == true)
                {

                }
            }
        }
    }
}

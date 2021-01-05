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

        public user GetEmail(string email)
        {
            return db.user.SingleOrDefault(x => x.email == email);
        }

        public int Insert(user entity)
        {
            db.user.Add(entity);
            db.SaveChanges();
            return entity.user_id;
        }

        public int InsertForFacebook(user entity)
        {
            var user = db.user.SingleOrDefault(x => x.email == entity.email);
            if (user == null)
            {
                db.user.Add(entity);
                db.SaveChanges();
                return entity.user_id;
            }
            else
            {
                return user.user_id;
            }
           
        }

        public int Login(string email, string password, bool hasLoggedAdmin = false)
        {
            var result = db.user.SingleOrDefault(x => x.email == email);
            if (result == null)
            {
                return 0; // Account does not exist
            }
            else
            {
                if (hasLoggedAdmin == true)
                {
                    if (result.status == false)
                    {
                        return -1; // Account locked
                    }
                    else
                    {
                        if (result.password == password)
                        {
                            return 1; // Login correctly
                        }
                        else
                        {
                            return -2; // Login incorrectly
                        }
                    }
                }
                else
                {
                    if (result.password == password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }
    }
}

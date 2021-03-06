﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
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

        public List <user> GetUserInfo(string email)
        {
            return db.user.Where(x => x.email == email).ToList();
        }

        public bool Update(user userEntity)
        {
            try
            {
                var userTable = db.user.Find(userEntity.user_id);
                if (!string.IsNullOrEmpty(userEntity.password))
                {
                    userTable.password = userEntity.password;
                }
                userTable.user_group_id = userEntity.user_group_id;
                userTable.display_name = userEntity.display_name;
                userTable.phone = userEntity.phone;
                userTable.edited_date = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public user ViewDetail(int id)
        {
            return db.user.Find(id);
        }

        public bool Remove(int id)
        {
            try
            {
                var userSelected = db.user.Find(id);
                db.user.Remove(userSelected);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<user> GetListUser(ref int totalRecord, int page, int pageSize)
        {
            totalRecord = db.user.Count();
            return db.user.OrderByDescending(x => x.created_date).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public bool CheckEmail(string email)
        {
            return db.user.Count(x => x.email == email) > 0;
        }

        public bool CheckPhone(string phone)
        {
            return db.user.Count(x => x.phone == phone) > 0;
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

        public int InsertForGoogle(user entity)
        {
            var user = db.user.SingleOrDefault(x => x.email == entity.email);
            if (user == null)
            {
                db.user.Add(entity);
                db.SaveChanges();
                return 1;
            }
            return user.user_id;
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
                if (hasLoggedAdmin == true)  // Đăng nhập bên trong admin
                {
                    if (result.user_group_id == ConstUserGroup.ADMIN_GROUP || result.user_group_id == ConstUserGroup.MODERATOR_GROUP)
                    {
                        if (result.status == false)
                        {
                            return -1; // Account locked
                        }
                        else
                        {
                            if (result.password == password)
                            {
                                return 1; // correct correctly
                            }
                            else
                            {
                                return -2; // wrong password
                            }
                        }
                    }
                    else
                    {
                        return -3; // login incorrect
                    }
                }
                else   // Đăng nhập bên ngoài
                {
                    if (result.status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.password == password)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models;
using Model.ADO;
using System.Web.Script.Serialization;
using Model.EF;
using System.Configuration;
using Common;
using FashionShop.Common;
using System.Xml.Linq;

namespace FashionShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[SessionConst.CART_SESSION];
            var listItems = new List<CartModel>();
            if (cart != null)
            {
                listItems = (List<CartModel>)cart;
            }
            return View(listItems);
        }

        [HttpGet]
        public ActionResult Order()
        {
            var cart = Session[SessionConst.CART_SESSION];
            var listItems = new List<CartModel>();
            if (cart != null)
            {
                listItems = (List<CartModel>)cart;
            }
            return PartialView(listItems);
        }
        [HttpPost]
        public ActionResult Order(string name, string email, string phone, string address, string province, string district)
        {
            var objOrder = new order();
            objOrder.order_name = name;
            objOrder.order_email = email;
            objOrder.order_phone = phone;
            objOrder.order_address = address;
            objOrder.order_province = province;
            objOrder.order_district = district;
           // objOrder.order_ward = ward;
            objOrder.order_date = DateTime.Now;

            // Get user infor
            try
            {
                var orderId = new OrderADO().Insert(objOrder);
                var cart = (List<CartModel>)Session[SessionConst.CART_SESSION];

                var orderDetailADO = new OrderDetailADO();
                decimal total = 0;

                var orderDetail = new order_detail();
                foreach (var item in cart)
                {
                    orderDetail.product_id = item.Product.product_id;
                    orderDetail.order_id = orderId;
                    orderDetail.price = item.Product.price;
                    orderDetail.quantity = item.Quantity;
                    orderDetailADO.Insert(orderDetail);
                    total += (item.Product.price.GetValueOrDefault(0) * item.Quantity);
                }
                var productInfo = new product();
                foreach (var item in cart)
                {
                    productInfo.prod_code = item.Product.prod_code;
                    productInfo.product_name = item.Product.product_name;
                    productInfo.quantity = item.Product.quantity;
                    productInfo.price = item.Product.price;
                }
              
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Contents/EmailTemplate/neworder.html"));
                content = content.Replace("{{CustomerName}}", name);
                content = content.Replace("{{CustomerPhone}}", phone);
                content = content.Replace("{{CustomerEmail}}", email);
                content = content.Replace("{{CustomerAddress}}", address);
                content = content.Replace("{{CustomerProvince}}", province);
                content = content.Replace("{{CustomerDistrict}}", district);
                //  content = content.Replace("{{CustomerWard}}", ward);
                content = content.Replace("{{CustomerProdCode}}", productInfo.prod_code);
                content = content.Replace("{{CustomerProdName}}", productInfo.product_name);
                content = content.Replace("{{CustomerQuantity}}", productInfo.quantity.ToString());
                content = content.Replace("{{CustomerPrice}}", productInfo.price.ToString());
                content = content.Replace("{{CustomerTotal}}", total.ToString("N0"));

                var toEmailAddress = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                new MailHelper().SendMail(email, "FashionShop - Xác nhận đơn hàng", content); // Send Email to Customer
                new MailHelper().SendMail(toEmailAddress, "FashionShop - Xác nhận đơn hàng", content); // Send Email to Admin

            }
            catch (Exception)
            {
                return Redirect("/dat-hang-khong-thanh-cong");
            }
            Session[SessionConst.CART_SESSION] = null;
            return Redirect("/dat-hang-thanh-cong");
        }

        // Load Province & District
        public JsonResult LoadProvince()
        {
            // Load dữ liệu trừ file xml
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Contents/data/provinces-data.xml"));
            // Truy vấn ra các trường Tỉnh/Thành phố trong file xml 
            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var list = new List<ProvinceModel>();
            ProvinceModel provinceModel = null;
            foreach (var item in xElements)
            {
                provinceModel = new ProvinceModel();
                provinceModel.Id = int.Parse(item.Attribute("id").Value);
                provinceModel.Name = item.Attribute("value").Value;
                list.Add(provinceModel);
            }
            return Json(new
            {
                data = list,
                status = true
            });
        }

        public JsonResult LoadDistrict(string provinceName)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Contents/data/provinces-data.xml"));
            // Lấy ra tỉnh theo tên truyền vào tương ứng
            var xElement = xmlDoc.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province"
                && (x.Attribute("value").Value) == provinceName);

            var list = new List<DistrictModel>();
            DistrictModel districtModel = null;
            // Vòng lặp các quận theo tỉnh
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                districtModel = new DistrictModel();
                districtModel.Id = int.Parse(item.Attribute("id").Value);
                districtModel.Name = item.Attribute("value").Value;
                districtModel.ProvinceId = int.Parse(xElement.Attribute("id").Value);
                list.Add(districtModel);
            }
            return Json(new
            {
                data = list,
                status = true
            });
        }

        //public JsonResult LoadWard(string districtName)
        //{
        //    var xmlDoc = XDocument.Load(Server.MapPath(@"/Contents/data/provinces-data.xml"));
        //    // Lấy ra Quận theo biến truyền vào tương ứng
        //    var xElement = xmlDoc.Elements("Root").Elements("Item").Elements("Item")
        //        .Single(x => x.Attribute("type").Value == "district"
        //        && (x.Attribute("type").Value == districtName));
        //    var list = new List<WardModel>();
        //    WardModel wardModel = null;
        //    // Vòng lặp các phường theo quận
        //    foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "precinct"))
        //    {
        //        wardModel = new WardModel();
        //        wardModel.Id = int.Parse(item.Attribute("id").Value);
        //        wardModel.Name = item.Attribute("value").Value;
        //        wardModel.DistrictId = int.Parse(xElement.Attribute("id").Value);
        //        list.Add(wardModel);
        //    }
        //    return Json(new
        //    {
        //        data = list,
        //        status = true

        //    }) ;
        //}

        public ActionResult SuccessNotification()
        {
            Session[SessionConst.CART_SESSION] = null;
            return View();
        }

        public ActionResult FailedNotification()
        {
            return View();
        }

        // Using Ajax Jquery update, delete Cart
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartModel>>(cartModel);
            var sessionCart = (List<CartModel>)Session[SessionConst.CART_SESSION];

            foreach (var item in sessionCart)
            {   // SingleOrDefault trả về phần từ duy nhất của chuỗi, or giá trị mặc định khi chuỗi trống.
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.product_id == item.Product.product_id);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[SessionConst.CART_SESSION] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(int product_id)
        {
            var sessionCart = (List<CartModel>)Session[SessionConst.CART_SESSION];
            sessionCart.RemoveAll(x => x.Product.product_id == product_id);
            Session[SessionConst.CART_SESSION] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public ActionResult _HeaderCartPartial()
        {
            var cart = Session[SessionConst.CART_SESSION];
            var listItems = new List<CartModel>();
            if (cart != null)
            {
                listItems = (List<CartModel>)cart;
            }
            return PartialView(listItems);
        }

        public ActionResult AddItems(int productId, int quantity)
        {
            // get product id
            var product = new ProductADO().GetProductId(productId);
            var cart = Session[SessionConst.CART_SESSION];

            if (cart != null) // giỏ hàng đã tồn tại -> cộng số lượng 
            {
                var listItems = (List<CartModel>)cart;
                if (listItems.Exists(x => x.Product.product_id == productId))
                {
                    foreach (var item in listItems)
                    {
                        if (item.Product.product_id == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else // giỏ hàng đã tồn tại nhưng khác sản phẩm -> tạo mới
                {
                    var item = new CartModel();
                    item.Product = product;
                    item.Quantity = quantity;
                    listItems.Add(item);
                }
            }
            else // giỏ hàng chưa tồn tại -> tạo mới
            {
                var item = new CartModel();
                item.Product = product;
                item.Quantity = quantity;
                var listItems = new List<CartModel>();
                listItems.Add(item);
                Session[SessionConst.CART_SESSION] = listItems; // Assign to session
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
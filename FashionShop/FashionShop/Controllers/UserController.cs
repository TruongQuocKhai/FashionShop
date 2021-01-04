using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models;
using Model.ADO;
using Model.EF;
using Common;

namespace FashionShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User


        public ActionResult _HeaderUserLoginPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var userAdo = new UserADO();
            var result = userAdo.
            if (ModelState.IsValid)
            {

            }
            else
            {
                ModelState.AddModelError("", "Đăng nhập khôn đúng!");
            }
        }



        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var userAdo = new UserADO();
                if (userAdo.CheckEmail(model.Email))
                {
                    // CheckEmail == true => Bắt lỗi email đã tồn tại
                    ModelState.AddModelError("Email", "Email đã tồn tại!");
                }
                else
                {
                    var user = new user();
                    user.display_name = model.DisplayName;
                    user.phone = model.Phone;
                    user.email = model.Email;
                    user.password = model.Password;
                    user.created_date = DateTime.Now;
                    user.status = true;
                    // gửi mail xác nhận cho user.
                    string emailMsg = "Dear " + model.Email +
                        ", <br /><br /><b/><h1>Chào mừng bạn đến với thời trang nam FashionShop!</h1>" +
                         "<br />Chúc mừng bạn đã kích hoạt tài khoản khách hàng thành công. Lần mua hàng tiếp theo, hãy đăng nhập để việc thanh toán thuận tiện hơn." +
                         "<br /><br /><a href='http://localhost:62132/'>Đến với cửa hàng của chúng tôi</a>" +
                         "<br /><br />Thanks & Regards, <br />FashionShop";
                    string emailSubject = EmailInfo.EMAIL_SUBJECT_DEFAULT;
                    await this.SendEmailAsync(model.Email, emailMsg, emailSubject);
                    var result = userAdo.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Successful = "Chúc mừng bạn đã đăng ký tài khoản khách hàng thành công!";
                        model = new RegistrationModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký tài khoản không thành công!");
                    }
                }
            }
            return View(model);
        }

        /// <summary>  
        ///  Send Email method.  
        /// </summary>  
        /// <param name="email">Email parameter</param>  
        /// <param name="msg">Message parameter</param>  
        /// <param name="subject">Subject parameter</param>  
        /// <returns>Return await task</returns>  
        public async Task<bool> SendEmailAsync(string email, string msg, string subject = "")
        {
            // Initialization.  
            bool isSend = false;

            try
            {
                // Initialization.  
                var body = msg;
                var message = new MailMessage();

                // Settings.  
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress(EmailInfo.FROM_EMAIL_ACCOUNT);
                message.Subject = !string.IsNullOrEmpty(subject) ? subject : EmailInfo.EMAIL_SUBJECT_DEFAULT;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    // Settings.  
                    var credential = new NetworkCredential
                    {
                        UserName = EmailInfo.FROM_EMAIL_ACCOUNT,
                        Password = EmailInfo.FROM_EMAIL_PASSWORD
                    };

                    // Settings.  
                    smtp.Credentials = credential;
                    smtp.Host = EmailInfo.SMTP_HOST_GMAIL;
                    smtp.Port = Convert.ToInt32(EmailInfo.SMTP_PORT_GMAIL);
                    smtp.EnableSsl = true;

                    // Sending  
                    await smtp.SendMailAsync(message);

                    // Settings.  
                    isSend = true;
                }
            }
            catch (Exception ex)
            {
                // Info  
                throw ex;
            }

            // info.  
            return isSend;
        }


    }
}
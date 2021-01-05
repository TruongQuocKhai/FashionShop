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
using FashionShop.Common;
using Facebook;
using System.Configuration;

namespace FashionShop.Controllers
{
    public class UserController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult FacebookLogin()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's informaiton like email, first name, last name,...
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string firstName = me.first_name;
                string middleName = me.middle_name;
                string lastName = me.last_name;

                var user = new user();
                user.email = email;
                user.display_name = lastName + " " + middleName + " " +  firstName;
                user.status = true;
                user.created_date = DateTime.Now;

                var insertResult = new UserADO().InsertForFacebook(user);
                if (insertResult > 0)
                {
                    var userSession = new LoginUser();
                    userSession.UserId = user.user_id;
                    userSession.DisplayName = user.display_name;
                    userSession.UserEmail = user.email;
                    Session.Add(SessionConst.USER_SESSION, userSession);
                }
            }
            return Redirect("/");
        }

        public ActionResult _HeaderLoginUserPartial()
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
            if (ModelState.IsValid)
            {
                var userAdo = new UserADO();
                var result = userAdo.Login(model.Email, Encryption.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = userAdo.GetEmail(model.Email);
                    var userSession = new LoginUser();
                    userSession.UserId = user.user_id;
                    userSession.DisplayName = user.display_name;
                    userSession.UserEmail = user.email;
                    Session.Add(SessionConst.USER_SESSION, userSession);
                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa!");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Tài khoản không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhâp không đúng!");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Response.Cookies.Clear();
            return Redirect("/");
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
                    user.password = Encryption.MD5Hash(model.Password);
                    user.created_date = DateTime.Now;
                    user.status = true;
                    // gửi mail xác nhận cho user.
                    string emailMsg = "Dear " + model.Email +
                        ", <br /><br /><b/><h1>Chào mừng bạn đến với cửa hàng thời trang nam FashionShop!</h1>" +
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

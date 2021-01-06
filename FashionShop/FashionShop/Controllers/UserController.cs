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
using Google;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

namespace FashionShop.Controllers
{
    public class UserController : Controller
    {
        // For Google
        private string ClientId = ConfigurationManager.AppSettings["Google.ClientID"];
        private string SecretKey = ConfigurationManager.AppSettings["Google.SecretKey"];
        private string RedirectUrl = ConfigurationManager.AppSettings["Google.RedirectUrl"];

        /// <summary>  
        /// Nhấn vào Google API để lấy mã truy cập  
        /// </summary>  
        public void LoginUsingGoogle()
        {
            Response.Redirect($"https://accounts.google.com/o/oauth2/v2/auth?client_id={ClientId}&response_type=code&scope=openid%20email%20profile&redirect_uri={RedirectUrl}&state=abcdef");
        }

        /// <summary>  
        /// Nghe phản hồi từ API Google sau khi user ủy quyền
        /// </summary>  
        /// <param name="code">Mã truy cập được trả về từ API google</param>  
        /// <param name="state">Một giá trị đc app chuyển qua ngăn tấn công giả mạo</param>  
        /// <param name="session_state">trạng thái phiên</param>  
        /// <returns></returns>  
        [HttpGet]
        public async Task<ActionResult> SaveGoogleUser(string code, string state, string session_state)
        {
            if (string.IsNullOrEmpty(code))
            {
                return View("Error");
            }

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.googleapis.com")
            };
            var requestUrl = $"oauth2/v4/token?code={code}&client_id={ClientId}&client_secret={SecretKey}&redirect_uri={RedirectUrl}&grant_type=authorization_code";

            var dict = new Dictionary<string, string>
            {
                { "Content-Type", "application/x-www-form-urlencoded" }
            };
            var req = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, requestUrl) { Content = new FormUrlEncodedContent(dict) };
            var response = await httpClient.SendAsync(req);
            var token = JsonConvert.DeserializeObject<GmailToken>(await response.Content.ReadAsStringAsync());
            //Session[SessionConst.USER_SESSION] = token.AccessToken;
            var obj = await GetuserProfile(token.AccessToken);

            var user = new user();
            user.email = obj.UserEmail;
            user.display_name = obj.GivenName;
            user.created_date = DateTime.Now;
            user.status = true;
            var insertResult = new UserADO().InsertForGoogle(user);
            if (insertResult > 0)
            {
                Session.Add(SessionConst.USER_SESSION, obj);
            }
            return Redirect("/");
        }

        /// <summary>  
        /// Để tìm nạp hồ sơ user bằng mã thông báo. 
        /// </summary>  
        /// <param name="accesstoken">Truy cập thẻ</param>  
        /// <returns>User Profile page</returns>  
        public async Task<UserProfile> GetuserProfile(string accesstoken)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.googleapis.com")
            };
            string url = $"https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={accesstoken}";
            var response = await httpClient.GetAsync(url);
            return JsonConvert.DeserializeObject<UserProfile>(await response.Content.ReadAsStringAsync());
        }


        // For facebook
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
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email,phone,picture");
                string email = me.email;
                string firstName = me.first_name;
                string middleName = me.middle_name;
                string lastName = me.last_name;
                //var token = JsonConvert.DeserializeObject(me.picture);
                //string picture = token;

                var user = new user();
                user.email = email;
                user.display_name = lastName + " " + middleName + " " + firstName;
                user.created_date = DateTime.Now;
                user.status = true;

                var insertResult = new UserADO().InsertForFacebook(user);
                if (insertResult > 0)
                {
                    var userProfile = new UserProfile();
                    userProfile.GivenName = user.display_name;
                    Session.Add(SessionConst.USER_SESSION, userProfile);
                }
                return Redirect("/");
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

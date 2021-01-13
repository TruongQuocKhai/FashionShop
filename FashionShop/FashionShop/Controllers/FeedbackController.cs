using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using FashionShop.Models;
using Model.ADO;
using Model.EF;

namespace FashionShop.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Contact

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FeedbackModel feedback)
        {
            var objFeedback = new feedback();
            objFeedback.feedback_name = feedback.FeedbackName;
            objFeedback.email = feedback.Email;
            objFeedback.subject = feedback.Subject;
            objFeedback.message = feedback.Message;

            try
            {
                var feedbackId = new FeedbackADO().Insert(objFeedback);

                string content = System.IO.File.ReadAllText(Server.MapPath("~/Contents/EmailTemplate/feedback.html"));
                content = content.Replace("{{CustomerName}}", feedback.FeedbackName);
                //content = content.Replace("{{CustomerEmail}}", feedback.Email);
                //content = content.Replace("{{CustomerSubject}}", feedback.Subject);
                content = content.Replace("{{CustomerMessage}}", feedback.Message);

                var toEmailAddress = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                new MailHelper().SendMail(feedback.Email, "FashionShop - Xác nhân feedback của bạn", content); // Send Email to Customer
                new MailHelper().SendMail(toEmailAddress, feedback.Subject, content); // Send Email to Admin
            }
            catch (Exception)
            {
                ViewBag.Failed = "Gửi feedback thất bại!";
            }
            ViewBag.Success = "Gửi feedback thành công!";
            return View();
        }
    }
}
﻿using Ecommerce.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(Email model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("mhender24@gotimetechnologies.com")); 
                message.To.Add(new MailAddress("mbolin@gotimetechnologies.com"));
                message.Subject = "Request from Contact Page";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
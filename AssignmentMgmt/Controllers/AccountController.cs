using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentMgmt.Model;
using AssignmentMgmt.Models;
using ClassWeb.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentMgmt.Controllers
{
    public class AccountController : BaseController
    {
        #region Private Variables
        //private readonly IEmailService _emailService; //Use classes to send email in serivices folder

        //hosting Envrironment is used to create the user directory 
        private IHostingEnvironment _hostingEnvironment;
        #endregion

        #region constructor
        public AccountController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion
        public IActionResult Register()
        {
            return View();
        }

        /// Created on: 03/07/2019
        /// Created By: Elvis
        /// Attempts to login the user with the provided username and password

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(String userName, String passWord)
        {
            User loggedIn = DAL.GetUser(userName, passWord);
            CurrentUser = loggedIn; //Sets the session for user from base controller

            if (loggedIn != null)
            {
                HttpContext.Session.SetString("username", userName);
                HttpContext.Session.SetInt32("UserID", loggedIn.ID); //Sets userid in the session
                                                                     //Check if the user is admin
                HttpContext.Session.SetString("UserRole", (loggedIn.Role.IsAdmin == true) ? "True" : "False");
                if (loggedIn.Role.IsAdmin)
                {
                    return RedirectToAction("Index", "Admin"); //Redirects to the admin dashboard
                }
                return RedirectToAction("Dashboard");
                //return View("Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid Username and/or Password";
                ViewBag.User = userName;
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
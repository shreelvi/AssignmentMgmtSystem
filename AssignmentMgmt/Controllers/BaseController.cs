using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentMgmt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClassWeb.Controllers
{
    /// <summary>
    /// Created by: Elvis
    /// Created on: 04/01/2019
    /// Reference: Prof.Holmes Peerval Project
    /// Taken code to manage user session and
    /// check permission in base controller
    /// </summary>
    public class BaseController : Controller
    {
        public BaseController()
        {
            //User u = await CurrentUser();
            //ViewBag.CurrentUser = u;
        }

        public string Get(string key)
        {
            string sObject = HttpContext.Session.GetString(key);
            return sObject;
        }
        public T Get<T>(string key)
        {
            //_context = context;
            if (HttpContext.Session.Keys.Contains(key))
            {
                string sObject = HttpContext.Session.GetString(key);
                return JsonConvert.DeserializeObject<T>(sObject);
            }
            else
            {
                return default(T);
            }
        }
        public void Set(string key, object obj)
        {
            String jsonString = JsonConvert.SerializeObject(obj);
            HttpContext.Session.SetString(key, jsonString);
        }

        internal User CurrentUser
        {
            get
            {
                User u = Get<User>("CurrentUser");
                if (u == null) u = new User()
                {
                    FirstName = "Anony",
                    Role = new Role()
                    {
                        Name = "Anonymous",
                    }
                };
                return u;
            }
            set
            {
                Set("CurrentUser", value);
            }
        }
       
        //Same method as above. Created because we used loginmodel
        internal User LoggedInUser
        {
            get
            {
                User u = Get<User>("LoggedInUser");
                if (u == null) u = new User() { FirstName = "Anony" };
                return u;
            }
            set
            {
                Set("LoggedInUser", value);
            }
        }

        internal bool UserCan<T>(PermissionSet.Permissions perm)
        {
            User user = CurrentUser;
            if (user == null) return false;
            if (typeof(T) == typeof(Assignment))
            {
                //List<Role> rr = DAL.GetRoles();
                //Role r = user.Role;
                //PermissionSet n = r.Assignment;
                return user.Role.Assignment >= perm;
            }
            else if (typeof(T) == typeof(Role))
            {
                Role r = user.Role;
                PermissionSet nn = r.Roles;
                return user.Role.Roles >= perm;
            }
            else if (typeof(T) == typeof(User))
            {
                return user.Role.Users >= perm;
            }
            else
                return false;
        }
    }
}
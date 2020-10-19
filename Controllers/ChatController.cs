using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebCommunity.Models;

namespace WebCommunity.Controllers
{
    public class ChatController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        public ChatController(UserManager<ApplicationUser> usrMgr)
        {
            userManager = usrMgr;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Chat()
        {
            //var userName = userManager.GetUserAsync();
            //ApplicationUser user = userManager.GetUserNameAsync();
            //if (user != null)
            //{
            //    if (!string.IsNullOrEmpty(userName))
            //        user.UserName = userName;

            //    return View(user);
            //}
            //else
                return View();
        }
    }
}

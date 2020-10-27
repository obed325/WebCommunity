using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WebCommunity.Data;
using WebCommunity.Models;

namespace WebCommunity.Controllers
{
    public class ChatController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _context;

        public ChatController(UserManager<ApplicationUser> usrMgr, ApplicationDbContext context)
        {
            userManager = usrMgr;
            _context = context;
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

        [HttpPost]
        public IActionResult SaveChat(string parameter1, string identityName)
        {

            var chatText = parameter1;

            Models.Chat newMessage = new Chat();
            newMessage.ChatHistory = chatText;
            newMessage.IdName = identityName;

            if (ModelState.IsValid)
            {
                _context.Chats.Add(newMessage);
                _context.SaveChanges();
            }

            return View();
        }

        public string GetChatConversations(string identityName)
        {
            var chatMatches = _context.Chats.Where(x => x.IdName == identityName).Select(x => x.ChatHistory).Take(10);

            string messageHistory = "";
            foreach(string item in chatMatches)
            {
                messageHistory += item;
            }

            return messageHistory;
        }

        //[HttpGet]
        //public ActionResult LoadChat()
        //{
            //string chatHistory = _context.Chat();
            //string chatHistory = "";

            //return Json(chatHistory, Newtonsoft.Json.JsonSerializerSettings());// JsonRequestBehavior.AllowGet);
        
        //}
    }
}

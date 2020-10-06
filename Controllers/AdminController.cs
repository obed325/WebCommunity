using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using WebCommunity.Models;
using WebCommunity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebCommunity.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        private UserManager<ApplicationUser> userManager;
        private IPasswordHasher<ApplicationUser> passwordHasher;
        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> usrMgr,IPasswordHasher<ApplicationUser> passwordHash)
        {
            _context = context;
            userManager = usrMgr;
            passwordHasher = passwordHash;
        }

        // GET: AdminController
        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(userManager.Users);
        }


        [Authorize]
        public ActionResult UserList()
        {
            return View(userManager.Users);
        }

        // GET: AdminController/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
            return View();
        }

        // GET: AdminController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            //ViewData["Cities"] = new SelectList(_context.Cities, "Value", "Text");

            //var toSelect = new SelectList(_context.Cities, "Id", "Name");
            //List<City> cities = _context.Cities.ToList();
            
           
            //List<SelectListItem> citySelection;
            //citySelection = _context.Cities.Select(a =>
            //            new SelectListItem
            //            {
            //                Value = a.Id.ToString(),
            //                Text = a.Name
            //            }).ToList();

               

            //ViewData["Cities"] = citySelection;
            //ViewBag.Cities = citySelection;

            return View();
        }

        // POST: AdminController/Create
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(UserVM user)
        {
            if (ModelState.IsValid)
            {
                //City rightCityObject = _context.Cities.Where(x => x.Id == user.Value).FirstOrDefault();

                ApplicationUser appUser = new ApplicationUser
                {
                    //City = rightCityObject,
                    UserName = user.Name,
                    Email = user.Email

                };


                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        // GET: AdminController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", userManager.Users);
        }
      
    }
}

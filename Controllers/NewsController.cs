using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCommunity.Data;
using WebCommunity.Helpers;
using WebCommunity.Models;


namespace WebCommunity.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _env; //håller reda på sökvägar
        private IDeleteImages _deleteImages;

        public NewsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment env, IDeleteImages deleteImages)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
            _deleteImages = deleteImages;
        }

        // GET: NewsController
        public IActionResult Index()
        {
          
            NewsVM newsVM = new NewsVM();
            newsVM.AllNews = _context.News.OrderBy(n => n.Created).ToList();

            return View(newsVM);

        }
        //public IActionResult View(int id)
        //{

        //    NewsVM newsVM = new NewsVM();
        //    newsVM.AllNews = _context.News.OrderBy(n => n.Created).ToList();

        //    return View(newsVM);
        //}


        // GET: NewsController/Details/5
        public async Task<IActionResult> ViewAsync(int? id)
        {
            News news = new News();
            news = await _context.News.FirstOrDefaultAsync(n => n.Id == id);

            return View(news);
        }

        // GET: NewsController/Details/5
        public async Task<IActionResult> NewsItem()
        {
            News news = new News();
            news = _context.News.OrderByDescending(n => n.Created).FirstOrDefault();
            return View(news);

            //News news = new News();
            //news = await _context.News.OrderBy(n=>n.Created).Take(1);

            //return View(news);
        }
        public async Task<IActionResult> GetNews(HomeIndexModel him, string searchString)
        {

            NewsVM nVM = new NewsVM();

            nVM.AllNews = _context.News.OrderBy(n => n.Created).ToList();

            if (!string.IsNullOrEmpty(searchString) )
            {
                nVM.AllNews = _context.News.Where(s => s.NewsText.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            else if (!string.IsNullOrEmpty(searchString))
            {
                nVM.AllNews = _context.News.Where(s => s.Category.Contains(searchString)).ToList();
            }
            else if (him.NewsBackwards == true)
            {
                nVM.AllNews = _context.News.OrderByDescending(n => n.Created).ToList();
            }
            else
            {
                nVM.AllNews = _context.News.ToList();
            }
            return View(nVM);
        }


        // GET: NewsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        public ActionResult Create(NewsVM newsVM, IFormFile UploadFile)
        {
            var _news = new Models.News();

            if (UploadFile != null) //&& (UploadFile.Length > 0))
            {

                var fileExtension = Path.GetExtension(UploadFile.FileName);
                string pictureFolderPath = ImagePathBuilder.NewPath(_env.WebRootPath);
                var _picGuid = Guid.NewGuid().ToString();
                var picturePath = (Path.Combine(pictureFolderPath, (_picGuid))) + fileExtension;

                using (FileStream stream = new FileStream(picturePath, FileMode.Create))
                {
                    UploadFile.CopyTo(stream);
                }

                int startPosToSelect = picturePath.IndexOf("wwwroot\\Upload", System.StringComparison.CurrentCultureIgnoreCase);
                string relativePicturePath = picturePath.Substring(startPosToSelect + 8); //cut after "wwwroot/"

                _news.PictureUrl = relativePicturePath;
                _news.PicGuid = _picGuid;
                _news.PicName = newsVM.PicName;
            }

            try
            {
                
                _news.Author = newsVM.Author;
                _news.Headline = newsVM.Headline;
                _news.NewsText = newsVM.NewsText;
                _news.Created = DateTime.Now;

                _context.News.Add(_news);
                _context.SaveChanges();

                return RedirectToAction(nameof(View), new { _news.Id });
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: NewsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsController/Delete/5
        public async Task<ActionResult>DeleteAsync(int? id)
        {

            News news = await _context.News.FirstOrDefaultAsync(m => m.Id == id);

            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int? id, IFormCollection collection)
        {

            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                News news = new News();
                news = await _context.News.FindAsync(id);

                //kills belonged pictures
                if ((news.PicGuid != null)) // || (news.PicGuid.Length > 0 )|| (news.PictureUrl.Length > 0))
                {
                    _deleteImages.Delete(news.PictureUrl, news.PicGuid);
                }

                if (news != null)
                {
                    _context.News.Remove(news);
                    await _context.SaveChangesAsync();
                }

                //return RedirectToPage("./Index");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
                //return View("Index");
            }
        }
    }
}

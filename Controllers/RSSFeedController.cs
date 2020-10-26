using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using WebCommunity.Models;

namespace WebCommunity.Controllers
{
    public class RSSFeedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string RSSURL)
        {
            WebClient wclient = new WebClient();
            string RSSData = wclient.DownloadString(RSSURL);

            XDocument xml = XDocument.Parse(RSSData);
            var RSSFeedData = (from x in xml.Descendants("item")
                               select new RSSFeed
                               {
                                   Title = ((string)x.Element("title")),
                                   Link = ((string)x.Element("link")),
                                   Description = ((string)x.Element("description")),
                                   PubDate = ((string)x.Element("pubDate"))
                               });
            ViewBag.RSSFeed = RSSFeedData;
            ViewBag.URL = RSSURL;
            return View();
        }
    }
}

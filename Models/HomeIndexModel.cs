using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCommunity.Controllers;


namespace WebCommunity.Models
{
    public class HomeIndexModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<PostListingModel> LatestPosts { get; set; }

        public List<News> LatestNews { get; set; }

        public bool NewsBackwards { get; set; }

    }
}

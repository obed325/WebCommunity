using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCommunity.Data;
using WebCommunity.Models;

namespace WebCommunity.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;
        //private readonly IUpload _uploadService;
        //private readonly IConfiguration _configuration;
        public ForumController(IForum forumService, IPost postService)
        {
            _forumService = forumService;
            _postService = postService;
        }
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public IActionResult Index()
        {
            var forums = _forumService.GetAll()
                .Select(forum=> new ForumListingModel
                {
                    Id = forum.Id,
                    Name = forum.Title,
                    Description = forum.Description,
                    NumberOfPosts = forum.Posts?.Count()??0,
                    Latest = GetLatestPost(forum.Id) ?? new PostListingModel(),
                    NumberOfUsers = _forumService.GetActiveUsers(forum.Id).Count(),
                    ImageUrl = forum.ImageUrl,
                    HasRecentPost = _forumService.HasRecentPost(forum.Id)
                });

            var forumListingModels = forums as IList<ForumListingModel> ?? forums.ToList();

            var model = new ForumIndexModel
            {
                ForumList = forums
            };

            return View(model);
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public IActionResult Topic(int id, string searchQuery)
        {
            var forum = _forumService.GetById(id);
            //var posts = new List<Post>();
            var posts = _forumService.GetFilteredPosts(id, searchQuery).ToList();
            var noResults = (!string.IsNullOrEmpty(searchQuery) && !posts.Any());

            //if (!String.IsNullOrEmpty(searchQuery))
            //{
            posts = _postService.GetFilteredPosts(forum, searchQuery).ToList();
            //}
            //posts = forum.Posts.ToList();

            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                AuthorName = post.User.UserName,
                Title = post.Subject,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            }).OrderByDescending(post => post.DatePosted);

            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum),
                SearchQuery = searchQuery,
                EmptySearchResults = noResults
            };

            return View(model);
        }
        private ForumListingModel BuildForumListing(Forum forum)
        {

            return new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }
        public PostListingModel GetLatestPost(int forumId)
        {
            var post = _forumService.GetLatestPost(forumId);

            if (post != null)
            {
                return new PostListingModel
                {
                    AuthorName = post.User != null ? post.User.UserName : "",
                    DatePosted = post.Created.ToString(CultureInfo.InvariantCulture),
                    Title = post.Subject ?? ""
                };
            }

            return new PostListingModel();
        }
        //public IEnumerable<ApplicationUser> GetActiveUsers(int forumId)
        //{
        //    return _forumService.GetActiveUsers(forumId).Select(appUser => new ApplicationUser
        //    {
        //        Id = Convert.ToInt32(appUser.Id),
        //        ProfileImageUrl = appUser.ProfileImageUrl,
        //        Rating = appUser.Rating,
        //        Username = appUser.UserName
        //    });
        //}

        [HttpPost]
        public IActionResult Search(int id, string searchQuery)
        {
            return RedirectToAction("Topic", new { id, searchQuery });
        }
        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;

            return BuildForumListing(forum);
        }
        public IActionResult Create()
        {
            var model = new AddForumModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddForum(AddForumModel model)
        {
            //todo make proper picture upload
            var imageUri = "";

            if (model.ImageUpload != null)
            {
                //var blockBlob = PostForumImage(model.ImageUpload);
                //imageUri = blockBlob.Uri.AbsoluteUri;
            }

            else
            {
                imageUri = "/images/users/default.jpg";
            }

            var forum = new Models.Forum()
            {
                Title = model.Title,
                Description = model.Description,
                Created = DateTime.Now,
                ImageUrl = imageUri
            };

            await _forumService.Add(forum);
            return RedirectToAction("Index", "Forum");
        }

        //public CloudBlockBlob PostForumImage(IFormFile file)
        //{
        //    var connectionString = _configuration.GetConnectionString("AzureStorageAccountConnectionString");
        //    var container = _uploadService.GetBlobContainer(connectionString);
        //    var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
        //    var filename = Path.Combine(parsedContentDisposition.FileName.ToString().Trim('"'));
        //    var blockBlob = container.GetBlockBlobReference(filename);
        //    blockBlob.UploadFromStreamAsync(file.OpenReadStream());

        //    return blockBlob;
        //}

    }
}

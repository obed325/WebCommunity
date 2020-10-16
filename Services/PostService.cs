using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCommunity.Data;
using WebCommunity.Models;

namespace WebCommunity.Services
{
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task AddReply(PostReply reply)
        {
            _context.PostReplies.Add(reply);
            await _context.SaveChangesAsync();
        }

        public async Task Archive(int id)
        {
            var post = GetById(id);
            post.IsArchived = true;
            _context.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var post = GetById(id);
            _context.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task EditPostContent(int id, string newContent)
        {
            var post = GetById(id);
            post.Content = newContent;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetActiveUsers(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            var posts = _context.Posts
               .Include(post => post.User)
               .Include(post => post.Replies).ThenInclude(reply => reply.User)
               .Include(post => post.Forum);

            return posts;
        }

        public IEnumerable<ApplicationUser> GetAllUsers(IEnumerable<Post> posts)
        {
            var users = new List<ApplicationUser>();
            foreach (var post in posts)
            {
                users.Add(post.User);

                if (!post.Replies.Any()) continue;

                users.AddRange(post.Replies.Select(reply => reply.User));
            }
            return users.Distinct();
        }

        public Post GetById(int id)
        {
            return _context.Posts.Where(post => post.Id == id)
                .Include(post => post.Forum)
                .Include(post => post.User)
                .Include(post => post.Replies)
                .ThenInclude(reply=>reply.User).FirstOrDefault();
        }

        public IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery)
        {
            

            return string.IsNullOrEmpty(searchQuery)
                ? forum.Posts
                : forum.Posts.Where(post
                => post.Subject.Contains(searchQuery)
                || post.Content.Contains(searchQuery));
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            var query = searchQuery.ToLower();

            var posts = _context.Posts
                .Include(p => p.Forum)
                .Include(p => p.User)
                .Include(p => p.Replies)
                .Where(p => p.Subject.ToLower().Contains(query)
                 || p.Content.ToLower().Contains(query));
            return posts;

            //return GetAll().Where(p 
            //    => p.Subject.Contains(searchQuery)
            //    || p.Content.Contains(searchQuery));
        }

        //public IEnumerable<Post> GetFilteredPosts(int forumId, string modelSearchQuery)
        //{
        //    throw new NotImplementedException();
        //}

        public string GetForumImageUrl(int id)
        {
            var post = GetById(id);
            return post.Forum.ImageUrl;
        }

        public IEnumerable<Post> GetLatestPosts(int n)
        {
            return GetAll().OrderByDescending(p => p.Created).Take(n);
        }

        public IEnumerable<Post> GetPostsBetween(DateTime start, DateTime end)
        {
            return _context.Posts.Where(post => post.Created >= start && post.Created <= end);
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            var posts = _context.Forums
                .Where(forum => forum.Id == id).First()
                .Posts;

            return posts;
        }

        public IEnumerable<Post> GetPostsByForumId(int id)
        {
            return _context.Forums.First(forum => forum.Id == id).Posts;
        }

        public IEnumerable<Post> GetPostsByUserId(int id)
        {
            return _context.Posts.Where(post => post.User.Id == id.ToString());
        }

        public int GetReplyCount(int id)
        {
            return GetById(id).Replies.Count();
        }

        public bool HasRecentPost(int id)
        {
            throw new NotImplementedException();
        }

        public Task SetForumImage(int id, Uri uri)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumDescription(int id, string description)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int id, string title)
        {
            throw new NotImplementedException();
        }

       
    }
}

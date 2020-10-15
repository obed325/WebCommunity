﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCommunity.Data;
using WebCommunity.Models;

namespace WebCommunity.Services
{
    public class ForumService : IForum
    {
        private readonly ApplicationDbContext _context;
        private readonly IPost _postService;
        public ForumService(ApplicationDbContext context, IPost postService)
        {
            _context = context;
            _postService = postService;
        }

        public async Task Create(Forum forum)
        {
            _context.Add(forum);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int forumId)
        {
            var forum = GetById(forumId);
            _context.Remove(forum);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums
                .Include(forum => forum.Posts);
        }

        public IEnumerable<ApplicationUser> GetActiveUsers(int forumId)
        {
            var posts = GetById(forumId).Posts;

            if (posts == null || !posts.Any())
            {
                return new List<ApplicationUser>();
            }

            return _postService.GetAllUsers(posts);
        }

        public Forum GetById(int id)
        {
            var forum = _context.Forums
                .Where(f => f.Id == id)
                .Include(f => f.Posts)
                .ThenInclude(f => f.User)
                .Include(f => f.Posts)
                .ThenInclude(p => p.Replies)
                .ThenInclude(r => r.User)
                .Include(f=>f.Posts)
                .ThenInclude(p=>p.Forum)
                .FirstOrDefault();

            //var forum = _context.Forums
            //    .Where(f => f.Id == id)
            //    .Include(f => f.Posts)
            //    .ThenInclude(f => f.User)
            //    .Include(f => f.Posts)
            //    .ThenInclude(f => f.Replies)
            //    .ThenInclude(f => f.User)
            //    .Include(f => f.Posts)
            //    .ThenInclude(p => p.Forum)
            //    .FirstOrDefault();

            //if (forum.Posts == null)
            //{
            //    forum.Posts = new List<Post>();
            //}

            return forum;
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            return _postService.GetFilteredPosts(searchQuery);
        }

        public IEnumerable<Post> GetFilteredPosts(int forumId, string searchQuery)
        {
            if (forumId == 0) return _postService.GetFilteredPosts(searchQuery);

            var forum = GetById(forumId);

            return string.IsNullOrEmpty(searchQuery)
                ? forum.Posts
                : forum.Posts.Where(post
                    => post.Subject.Contains(searchQuery) || post.Content.Contains(searchQuery));
        }

        public async Task UpdateForumDescription(int id, string description)
        {
            var forum = GetById(id);
            forum.Description = description;

            _context.Update(forum);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateForumTitle(int id, string title)
        {
            var forum = GetById(id);
            forum.Title = title;

            _context.Update(forum);
            await _context.SaveChangesAsync();
        }

        public bool HasRecentPost(int id)
        {
            const int hoursAgo = 12;
            var window = DateTime.Now.AddHours(-hoursAgo);
            return GetById(id).Posts.Any(post => post.Created >= window);
        }

        public Post GetLatestPost(int forumId)
        {
            var posts = GetById(forumId).Posts;

            if (posts != null)
            {
                return GetById(forumId).Posts
                    .OrderByDescending(post => post.Created)
                    .FirstOrDefault();
            }
            return new Post();
        }

        public async Task Add(Forum forum)
        {
            _context.Add(forum);
            await _context.SaveChangesAsync();
        }

        public async Task SetForumImage(int id, Uri uri)
        {
            var forum = GetById(id);
            forum.ImageUrl = uri.AbsoluteUri;
            _context.Update(forum);
            await _context.SaveChangesAsync();
        }
    }
}

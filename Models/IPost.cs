using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommunity.Models
{
    public interface IPost
    {
        Post GetById(int id);
        //IEnumerable<Forum> GetAll();
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery);
        IEnumerable<Post> GetFilteredPosts(int forumId, string modelSearchQuery);
        IEnumerable<Post> GetPostsByForum(int id);
        IEnumerable<Post> GetLatestPosts(int n);

        IEnumerable<Post> GetPostsByUserId(int id);
        IEnumerable<Post> GetPostsByForumId(int id);
        IEnumerable<Post> GetPostsBetween(DateTime start, DateTime end);
        string GetForumImageUrl(int id);
        Task SetForumImage(int id, Uri uri);
        Task Archive(int id);
        Task AddReply(PostReply reply);
        int GetReplyCount(int id);
        bool HasRecentPost(int id);
        //Task Add(Forum forum);
        Task Add(Post post);
        //Task Create(Models.Forum forum);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);
        Task UpdateForumTitle(int id, string title);
        Task UpdateForumDescription(int id, string description);
        IEnumerable<ApplicationUser> GetAllUsers(IEnumerable<Post> posts);
        IEnumerable<ApplicationUser> GetActiveUsers(int forumId);
        IEnumerable<Post> GetFilteredPosts(string searchQuery);

        
    }
}

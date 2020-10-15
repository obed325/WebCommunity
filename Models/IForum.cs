using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCommunity.Data;

namespace WebCommunity.Models
{
    public interface IForum
    {
        Forum GetById(int id);
        IEnumerable<Forum> GetAll();
        IEnumerable<ApplicationUser> GetActiveUsers(int forumId);
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetFilteredPosts(int forumId, string modelSearchQuery);
        bool HasRecentPost(int id);
        Post GetLatestPost(int forumId);
        Task Create(Forum forum);
        Task Delete(int fourumId);
        Task UpdateForumTitle(int forumId, string newTitle);
        Task UpdateForumDescription(int forumId, string newDescription);
        Task Add(Models.Forum forum);
        Task SetForumImage(int id, Uri uri);
    }
}

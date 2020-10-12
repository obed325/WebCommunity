using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommunity.Models
{
    public interface IPost
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);
        IEnumerable<Post> GetLatestPosts(int n);

        Task Add(Post post);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);
        IEnumerable<ApplicationUser> GetAllUsers(IEnumerable<Post> posts);

        //Task AddReply(PostReply reply);

    }
}

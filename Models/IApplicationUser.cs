using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommunity.Models
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();

        Task IncrementRating(string id);
        Task Add(ApplicationUser user);
        Task Deactivate(ApplicationUser user);
        Task SetProfileImage(string id, Uri uri);
        Task BumpRating(string userId, Type type);
    }
}

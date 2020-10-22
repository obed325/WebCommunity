using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommunity.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string ChatHistory { get; set; }
        public string IdName { get; set; }

        //public virtual ApplicationUser User { get; set; }
    }
}

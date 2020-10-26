using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommunity.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please don't forget the headline")]
        public string Headline { get; set; }

        [Required(ErrorMessage = "Please don't forget the news")]
        public string NewsText { get; set; }

        public DateTime? Created { get; set; }

        public string PictureUrl { get; set; } = "";

        [StringLength(100)]
        public string PicName { get; set; } = "";
        public string PicGuid { get; set; } = "";

        [StringLength(50)]
        public string Author { get; set; }

        public string Category { get; set; }

    }
}


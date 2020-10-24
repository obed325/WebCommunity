using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommunity.Models
{
    public class NewsVM
    {
        public NewsVM()
        {
        }

        [BindProperty]
        public IFormFile UploadFile { get; set; }

        //[BindProperty]
        public string PicName { get; set; }
        public string PicGuid { get; set; }
        public string PictureUrl { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Please don't forget the headline")]
        public string Headline { get; set; }

        [Required(ErrorMessage = "Please don't forget the news")]
        public string NewsText { get; set; }

        public DateTime Created { get; set; }

        
        public string Author { get; set; }

        public virtual Category Category { get; set; }

        public List<News> AllNews { get; set; }
    }
}

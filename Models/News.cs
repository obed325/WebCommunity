using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string PictureUrl { get; set; } 

        [StringLength(100)]
        public string PicName { get; set; } 
        public string PicGuid { get; set; } 

        [StringLength(50)]
        public string Author { get; set; }

        public string Category { get; set; }

        [NotMapped]
        [BindProperty]
        public IFormFile UploadFile { get; set; }


        public List<News> PopulateNews()
        {
            List<News> newsList = new List<News>
            {
                new News
                {
                    //Id = 11,
                    Headline = "",
                    NewsText = "",
                    Created = Convert.ToDateTime("2020-01-02"),
                    Author = "",
                    PicName = "",
                    Category = "",
                },
                new News
                {
                    //Id = 12,
                    Headline = "",
                    NewsText = "",
                    Created = Convert.ToDateTime("2020-01-03"),
                    Author = "",
                    PicName = "",
                    Category = "",
                }

            };
            return newsList;
        }
    }
}


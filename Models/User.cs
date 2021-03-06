﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommunity.Models
{
    public class User
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]+{2,6}$", ErrorMessage = "E-mail is not valid")] //[\wåäöÅÄÖ]*
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
    }
}

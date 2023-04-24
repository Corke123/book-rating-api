using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace book_rating_api.Dtos
{
    public class AuthRequest
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(6, ErrorMessage = "Username must contain at least 6 characters")]
        public string username { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MinLength(6, ErrorMessage = "Username must contain at least 6 characters")]
        public string password { get; set; }
    }
}
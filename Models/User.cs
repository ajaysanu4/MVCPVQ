using System;
using System.ComponentModel.DataAnnotations;

namespace MVCTEST.Models
{
    public class User
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserID { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
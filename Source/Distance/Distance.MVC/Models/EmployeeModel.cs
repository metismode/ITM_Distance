using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Distance.MVC.Models
{
    public class EmployeeModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Username field is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password field is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "First name field is required.")]
        public string FirstName{ get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        public string NickName { get; set; }

        [Required(ErrorMessage = "Email field is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone field is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Role field is required.")]
        public int Role { get; set; }

        [Required(ErrorMessage = "Status field is required.")]
        public int Status { get; set; }

    }
}
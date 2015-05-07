using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Distance.MVC.Models
{
    public class MasterDataModel
    {
        public int Id { get; set; }

        public int PID { get; set; }

        public string NameP { get; set; }

        [Required(ErrorMessage = "Name field is required.")]
        public string Name { get; set; }

        [Display(Name="Latitude")]
        [Required(ErrorMessage = "Latitude field is required.")]
        public string Lat { get; set; }

        [Display(Name="Longitude")]
        [Required(ErrorMessage = "Longitude field is required.")]
        public string Lon { get; set; }

        [Required(ErrorMessage = "Status field is required.")]
        public int Status { get; set; }
    }
}
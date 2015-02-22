using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Distance.MVC.Models
{
    public class MasterDataModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Lat { get; set; }

        public string Lon { get; set; }

        public int Status { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Distance.MVC.Models
{
    public class OriDesModel
    {

        public int Id { get; set; }
        public string[] getO { get; set; }
        public string[] getD { get; set; }
        public string[] ori { get; set; }
        public string[] des { get; set; }
        public string[] distance { get; set; }
        public string[] duration { get; set; }
        public string[] keyworddes { get; set; }
        public string[] keywordori { get; set; }

    }
}
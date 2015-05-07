using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Distance.MVC.Models
{
    public class DropDownListModel
    {

        public List<DropDownDataModel> List { get; set; }
        public string SelectedValue { get; set; }
        public string SelectedProperty { get; set; }
        public string OptionalText { get; set; }
        public bool IsMultiselect { get; set; }
        public string CSSClass { get; set; }
        public bool HasSearch { get; set; }
        public bool Disabled { get; set; }
    }

    public class DropDownDataModel
    {
        public string Value { get; set; }
        public string Display { get; set; }
        public string Description { get; set; }
    }
}
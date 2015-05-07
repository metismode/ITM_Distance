using DX.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Distance.MVC.Models
{
    public class EmployeeListModel : PagedSortableList<EmployeeModel>
    {

        public List<EmployeeModel> List { get; set; }

    }
}
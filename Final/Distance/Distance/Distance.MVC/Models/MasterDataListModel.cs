using DX.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Distance.MVC.Models
{
    public class MasterDataListModel : PagedSortableList<EmployeeModel>
    {
        public List<MasterDataModel> List { get; set; }
    }
}
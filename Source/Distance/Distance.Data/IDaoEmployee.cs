using Distance.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Data
{
    public interface IDaoEmployee
    {

        List<Employee> GetEmployeeList(string sortExpresstion = "\"id\" ASC", string limitExpression = "", string offsetExpression = "0", string keyword = null, string filterData = null, int status = 0);
        int GetCount(string keyword = null, string filterData = null, int status = 0);
        Employee GetEmployee(int id);
        int InsertEmployee(Employee model);
        int UpdateEmployee(Employee model);
    }
}

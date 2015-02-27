using Distance.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Service.IServices
{
    public interface IServiceEmployee
    {
        List<Employee> GetEmployeeList(string sortExpression, out int TotalRows, int page = 1, int pageSize = 10, string keyword = null, string filterData = null, int status = 0);

        int InsertEmployee(Employee model);

        int UpdateEmployee(Employee model);

        Employee GetEmployee(int id);
    
    }
}

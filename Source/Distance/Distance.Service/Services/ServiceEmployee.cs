using Distance.Business;
using Distance.Data;
using Distance.Service.IServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Service.Services
{
    public class ServiceEmployee : IServiceEmployee
    {
        static readonly string provider = ConfigurationManager.AppSettings.Get("DataProvider");
        static readonly IDaoFactory factory = DaoFactories.GetFactory(provider);
        static readonly IDaoEmployee DaoEmployee = factory.DaoEmployee;


        private string GetOffset(int page, int pageSize)
        {
            return ((page - 1) == 0) ? (page - 1).ToString() : ((page * pageSize) - pageSize).ToString();
        }

        public List<Employee> GetEmployeeList(string sortExpression, out int TotalRows, int page, int pageSize, string keyword, string filterData = null, int status = 0)
        {

            string offset = GetOffset(page, pageSize);
            string limitExpression = pageSize.ToString();

            TotalRows = DaoEmployee.GetCount(keyword, filterData, status);

            return DaoEmployee.GetEmployeeList(sortExpression, limitExpression, offset, keyword, filterData, status);

        }

    }
}

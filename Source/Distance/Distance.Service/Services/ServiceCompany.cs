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
    public class ServiceCompany : IServiceCompany
    {
        static readonly string provider = ConfigurationManager.AppSettings.Get("DataProvider");
        static readonly IDaoFactory factory = DaoFactories.GetFactory(provider);
        //static readonly IDaoCompany DaoCompany = factory.DaoCompany;

        private string GetOffset(int page, int pageSize)
        {
            return ((page - 1) == 0) ? (page - 1).ToString() : ((page * pageSize) - pageSize).ToString();
        }

        //public List<Company> GetCompanyList(string sortExpression, out int TotalRows, int page, int pageSize, string keyword, string filterData = null, int status = 0)
        //{
        //    string offset = GetOffset(page, pageSize);
        //    string limitExpression = pageSize.ToString();
        //    TotalRows = DaoCompany.GetCount(keyword, filterData, status);


        //    return DaoCompany.GetCompanyList(sortExpression, limitExpression, offset, keyword, filterData, status);
        //}

        //public Company GetCompany(int id)
        //{
        //    return DaoCompany.GetCompany(id);
        //}

        //public int InsertCompany(Company model)
        //{
        //    model.CreatedDate = DateTime.Now;
        //    model.ChangedDate = DateTime.Now;

        //    return DaoCompany.InsertCompany(model);
        //}

        //public int UpdateCompany(Company model)
        //{
        //    model.ChangedDate = DateTime.Now;
        //    var id = DaoCompany.UpdateCompany(model);

        //    if (model.Status == 2)
        //    {
        //        var depid = DaoCompany.GetCompanyDepartmentId(model.Id);

        //        foreach (var item in depid)
        //        {
        //            DaoCompany.DeleteFKComDepLev(item.Id);
        //        }

        //        DaoCompany.DeleteFKComDep(model.Id);
        //    }

        //    return id;
        //}

        //public bool DeleteCompany(string[] id, int status)
        //{

        //    return DaoCompany.DeleteCompany(id, status);
        //}

    }
}

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
    public class ServiceMaster : IServiceMaster
    {
        static readonly string provider = ConfigurationManager.AppSettings.Get("DataProvider");
        static readonly IDaoFactory factory = DaoFactories.GetFactory(provider);
        static readonly IDaoMaster DaoMaster = factory.DaoMaster;


        private string GetOffset(int page, int pageSize)
        {
            return ((page - 1) == 0) ? (page - 1).ToString() : ((page * pageSize) - pageSize).ToString();
        }

        public List<Master> GetProvinceList(string sortExpression, out int TotalRows, int page, int pageSize, string keyword, string filterData = null, int status = 0)
        {

            string offset = GetOffset(page, pageSize);
            string limitExpression = pageSize.ToString();

            TotalRows = DaoMaster.GetCountP(keyword, filterData, status);

            return DaoMaster.GetProvinceList(sortExpression, limitExpression, offset, keyword, filterData, status);

        }

         public List<Master> GetAmphurList(string sortExpression, out int TotalRows, int page, int pageSize, string keyword, string filterData = null, int status = 0)
        {

            string offset = GetOffset(page, pageSize);
            string limitExpression = pageSize.ToString();

            TotalRows = DaoMaster.GetCountA(keyword, filterData, status);

            return DaoMaster.GetAmphurList(sortExpression, limitExpression, offset, keyword, filterData, status);

        }
         public List<Master> GetTambonList(string sortExpression, out int TotalRows, int page, int pageSize, string keyword, string filterData = null, int status = 0)
        {

            string offset = GetOffset(page, pageSize);
            string limitExpression = pageSize.ToString();

            TotalRows = DaoMaster.GetCountT(keyword, filterData, status);

            return DaoMaster.GetTambonList(sortExpression, limitExpression, offset, keyword, filterData, status);

        }



          

    }
}

using Distance.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Service.IServices
{
    public interface IServiceMaster
    {
        List<Master> GetProvinceList(string sortExpression, out int TotalRows, int page = 1, int pageSize = 10, string keyword = null, string filterData = null, int status = 0);
        List<Master> GetAmphurList(string sortExpression, out int TotalRows, int page = 1, int pageSize = 10, string keyword = null, string filterData = null, int status = 0);
        List<Master> GetTambonList(string sortExpression, out int TotalRows, int page = 1, int pageSize = 10, string keyword = null, string filterData = null, int status = 0);
    }
}

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

        Master GetProvince(int id);
        int InsertProvince(Master model);
        int UpdateProvince(Master model);

        Master GetAmphur(int id);
        int InsertAmphur(Master model);
        int UpdateAmphur(Master model);
    
    }
}

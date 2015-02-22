using Distance.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Data
{
    public interface IDaoMaster
    {
        List<Master> GetProvinceList(string sortExpresstion = "\"id\" ASC", string limitExpression = "", string offsetExpression = "0", string keyword = null, string filterData = null, int status = 0);
        List<Master> GetAmphurList(string sortExpresstion = "\"id\" ASC", string limitExpression = "", string offsetExpression = "0", string keyword = null, string filterData = null, int status = 0);
        List<Master> GetTambonList(string sortExpresstion = "\"id\" ASC", string limitExpression = "", string offsetExpression = "0", string keyword = null, string filterData = null, int status = 0);
        int GetCountP(string keyword = null, string filterData = null, int status = 0);
        int GetCountA(string keyword = null, string filterData = null, int status = 0);
        int GetCountT(string keyword = null, string filterData = null, int status = 0);
    }
}

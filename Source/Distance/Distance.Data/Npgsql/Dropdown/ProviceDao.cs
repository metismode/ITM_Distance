using Distance.Business.Dropdown;
using Distance.Data.Dropdown;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Data.Npgsql.Dropdown
{
    public class ProvinceDao : IProvinceDao
    {
        static Db db = new Db();

        public List<DDProvince> GetProvinceList()
        {
            string sql = "SELECT id,name,CONCAT(lon,',',lat) as ll " +
                         "FROM master_province";
            return db.Read(sql, Make).ToList();
        }

        static Func<IDataReader, DDProvince> Make = reader => new DDProvince
        {
            LatLon = reader["id"].AsString() + "/" + reader["ll"].AsString(),
            Name = reader["name"].AsString()

        };
    }
}

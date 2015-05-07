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
    public class AmphurDao : IAmphurDao
    {
        static Db db = new Db();

        public List<DDAmphur> GetAmphurList(int id)
        {
            string sql = "SELECT id,name,pid,CONCAT(lat,',',lon) as ll  " +
                         "FROM master_amphur WHERE pid ="+id;
            return db.Read(sql, Make).ToList();
        }

        static Func<IDataReader, DDAmphur> Make = reader => new DDAmphur
        {
            LatLon = reader["id"].AsString() + "/" + reader["ll"].AsString(),
            Name = reader["name"].AsString()

        };
    }
}

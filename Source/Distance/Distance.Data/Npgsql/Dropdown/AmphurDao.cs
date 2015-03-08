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

        public List<DDAmphur> GetAmphurList()
        {
            string sql = "SELECT id,name " +
                         "FROM master_amphur";
            return db.Read(sql, Make).ToList();
        }

        static Func<IDataReader, DDAmphur> Make = reader => new DDAmphur
        {
            LatLon = reader["id"].AsString(),
            Name = reader["name"].AsString()

        };
    }
}

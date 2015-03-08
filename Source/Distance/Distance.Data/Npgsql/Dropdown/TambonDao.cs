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
    public class TambonDao : ITambonDao
    {
        static Db db = new Db();

        public List<DDTambon> GetTambonList()
        {
            string sql = "SELECT id,name " +
                         "FROM master_tambon";
            return db.Read(sql, Make).ToList();
        }

        static Func<IDataReader, DDTambon> Make = reader => new DDTambon
        {
            LatLon = reader["id"].AsString(),
            Name = reader["name"].AsString()

        };
    }
}

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
    public class StatusDao : IStatusDao
    {

        static Db db = new Db();

        public List<DDStatus> GetStatusList()
        {
            string sql = "SELECT id,name " +
                         "FROM status";
            return db.Read(sql, Make).ToList();
        }

        static Func<IDataReader, DDStatus> Make = reader => new DDStatus
        {
            id = reader["id"].AsId(),
            name = reader["name"].AsString()

        };
    }
}

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
    public class RoleDao : IRoleDao
    {
        static Db db = new Db();

        public List<DDRole> GetRoleList()
        {
            string sql = "SELECT id,name " +
                         "FROM role";
            return db.Read(sql, Make).ToList();
        }

        static Func<IDataReader, DDRole> Make = reader => new DDRole
        {
            id = reader["id"].AsId(),
            name = reader["name"].AsString()

        };
    }
}

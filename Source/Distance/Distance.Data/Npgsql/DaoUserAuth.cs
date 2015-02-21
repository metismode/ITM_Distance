using Distance.Business;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Data.Npgsql
{
    class DaoUserAuth : IDaoUserAuth
    {
        static Db conn = new Db();

        public UserAuth GetUserByUsername(string username)
        {
            try
            {
                //string sql = "SELECT * FROM user_detail WHERE username = @username";

                string sql = "SELECT " +
                                "ud.id AS id,  " +
                                "ud.username AS username, " +
                                "ud.password AS password " +
                               
                                " FROM user_detail ud " +

                                "WHERE username = @username " +
                                "AND ud.status = 1 ";

                object[] parms = { new NpgsqlParameter("@username", DbType.String), username };

                return conn.Read(sql, Make, parms).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static Func<IDataReader, UserAuth> Make = reader => new UserAuth
        {
            UID = reader["id"].AsId(),
            Username = reader["username"].AsString(),
            Password = reader["password"].AsString(),
           
        };


    }
}

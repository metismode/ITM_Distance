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
    class DaoEmployee : IDaoEmployee
    {
        static Db db = new Db();

        public List<Employee> GetEmployeeList(string sortExpresstion = "\"id\" ASC", string limitExpression = "", string offsetExpression = "0", string keyword = null, string filterData = null, int status = 0)
        {
            string sql = "SELECT id, " +
                            "username, " +
                            "firstname, " +
                            "lastname, " +
                            "nickname, " +
                            "email, " +
                            "phone, " +
                            "status " +
                            "FROM user_detail ";

            sql += " ".OrderBy(sortExpresstion).Limit(limitExpression).Offset(offsetExpression);


            return db.Read(sql, MakeList).ToList();

        }

        public int GetCount(string keyword = null, string filterData = null, int status = 0)
        {
            string SQL = "SELECT count(*) FROM user_detail ";
            return db.Scalar(SQL).AsInt();
        }

        static Func<IDataReader, Employee> MakeList = reader => new Employee
        {
            id = reader["id"].AsInt(),
            UserName = reader["username"].AsString(),
            FirstName = reader["firstname"].AsString(),
            LastName = reader["lastname"].AsString(),
            NickName = reader["nickname"].AsString(),
            Email = reader["email"].AsString(),
            Phone = reader["phone"].AsString(),
            Status = reader["status"].AsInt()
           // StatusName = reader["status"].AsInt()
        };

    }




}

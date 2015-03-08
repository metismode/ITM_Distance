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


        public Employee GetEmployee(int id)
        {
            string sql = "select id,username,password,firstname,lastname,nickname,phone,status,email from user_detail WHERE id = " + id;

            return db.Read(sql, Make).FirstOrDefault();
        }

        public int InsertEmployee(Employee model)
        {

            string sql = "INSERT INTO user_detail (" +

                        " username," +
                        " password," +
                        " firstname, lastname," +
                        " nickname, " +
                        " phone," +
                        " status," +
                        " email )" +
                       " VALUES (" +
                       " @username," +
                       " @password," +
                       " @firstname ,@lastname," +
                       " @nickname  ," +
                       " @phone," +
                       " @status," +
                       " @email )";

            return db.Insert(sql, Take(model)).AsInt();
        }

        public int UpdateEmployee(Employee model)
        {
            string sql = " UPDATE user_detail " +
                        " SET  username=@username, " +
                        "password=@password, " +
                        "firstname=@firstname , " +
                        "lastname=@lastname, " +
                        "nickname=@nickname , " +
                        "phone=@phone, " +
                        "status=@status, " +
                        "email=@email  " +
                        " WHERE id =@id ";

            return db.Update(sql, Take(model)).AsInt();
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
           
        };
        static Func<IDataReader, Employee> Make = reader => new Employee
        {
            id = reader["id"].AsInt(),
            UserName = reader["username"].AsString(),
            Password = reader["password"].AsString(),
            FirstName = reader["firstname"].AsString(),
            LastName = reader["lastname"].AsString(),
            NickName = reader["nickname"].AsString(),
            Email = reader["email"].AsString(),
            Phone = reader["phone"].AsString(),
            Status = reader["status"].AsInt()
            
        };

        object[] Take(Employee employee)
        {
            return new object[]{
                new NpgsqlParameter("@id",DbType.Int32),employee.id,
                new NpgsqlParameter("@username",DbType.String),employee.UserName,
                new NpgsqlParameter("@password",DbType.String),employee.Password,                
                new NpgsqlParameter("@firstname",DbType.String),employee.FirstName,
                new NpgsqlParameter("@lastname",DbType.String),employee.LastName,
                new NpgsqlParameter("@nickname",DbType.String),employee.NickName,
                new NpgsqlParameter("@phone",DbType.String),employee.Phone,
                new NpgsqlParameter("@status",DbType.Int32),employee.Status,
                new NpgsqlParameter("@email",DbType.String),employee.Email
            };
        
        }


    }




}

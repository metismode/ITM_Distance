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
    class DaoMaster : IDaoMaster
    {
        static Db db = new Db();

        public List<Master> GetProvinceList(string sortExpresstion = "\"id\" ASC", string limitExpression = "", string offsetExpression = "0", string keyword = null, string filterData = null, int status = 0)
        {
            string sql = "SELECT id, " +
                            "name, " +
                            "lat, " +
                            "lon, " +
                            "status " +
                            "FROM master_province ";

            if (!string.IsNullOrEmpty(keyword))
            {

                sql += " WHERE ";

                if (filterData.Equals("name"))
                {
                    sql += "name LIKE @keyword ";
                }
                else
                {
                    sql += " (name LIKE @keyword) ";
                }
                keyword = keyword.Trim();
            }


            sql += " ".OrderBy(sortExpresstion).Limit(limitExpression).Offset(offsetExpression);

            object[] paramsData = {
                                  new NpgsqlParameter("@keyword", DbType.String), '%' + keyword + '%'
                                  };

            return db.Read(sql, MakeListP, paramsData).ToList();
        }
        public List<Master> GetAmphurList(string sortExpresstion = "\"idA\" ASC", string limitExpression = "", string offsetExpression = "0", string keyword = null, string filterData = null, int status = 0)
        {
            string sql = "SELECT ma.id as idA,mp.name as nameP,ma.name as nameA, " +
                            "pid, " +
                            "ma.lat as latA, " +
                            "ma.lon as lonA, " +
                            "ma.status as statusA " +
                            "FROM master_amphur ma " +
                            "LEFT JOIN master_province mp ON ma.pid = mp.id ";

            if (!string.IsNullOrEmpty(keyword))
            {

                sql += " WHERE ";

                if (filterData.Equals("nameP"))
                {
                    sql += "mp.name LIKE @keyword ";
                }
                else if (filterData.Equals("nameA"))
                {
                    sql += " ma.name LIKE @keyword ";
                }
                else
                {
                    sql += " (mp.name LIKE @keyword OR ma.name LIKE @keyword) ";
                }
                keyword = keyword.Trim();
            }



            sql += " ".OrderBy(sortExpresstion).Limit(limitExpression).Offset(offsetExpression);

            object[] paramsData = {
                                  new NpgsqlParameter("@keyword", DbType.String), '%' + keyword + '%'
                                  };

            return db.Read(sql, MakeListA, paramsData).ToList();
        }
        public List<Master> GetTambonList(string sortExpresstion = "\"id\" ASC", string limitExpression = "", string offsetExpression = "0", string keyword = null, string filterData = null, int status = 0)
        {
            string sql = "SELECT id, " +
                            "name, " +
                            "lat, " +
                            "lon, " +
                            "status " +
                            "FROM master_tambon ";

            sql += " ".OrderBy(sortExpresstion).Limit(limitExpression).Offset(offsetExpression);
            return db.Read(sql, MakeListP).ToList();
        }

        public int GetCountP(string keyword = null, string filterData = null, int status = 0)
        {
            string sql = "SELECT count(*) FROM master_province ";

            if (!string.IsNullOrEmpty(keyword))
            {

                sql += " WHERE ";

                if (filterData.Equals("name"))
                {
                    sql += "name LIKE @keyword ";
                }
                else
                {
                    sql += " (name LIKE @keyword) ";
                }
                keyword = keyword.Trim();
            }

            object[] paramsData = {
                                  new NpgsqlParameter("@keyword", DbType.String), '%' + keyword + '%'
                                  };

            return db.Scalar(sql, paramsData).AsInt();
        }
        public int GetCountA(string keyword = null, string filterData = null, int status = 0)
        {
            string sql = "SELECT count(*) FROM master_amphur ma ";

           sql += "LEFT JOIN master_province mp ON ma.pid = mp.id ";

            if (!string.IsNullOrEmpty(keyword))
            {

                sql += " WHERE ";

                if (filterData.Equals("nameP"))
                {
                    sql += "mp.name LIKE @keyword ";
                }
                else if (filterData.Equals("nameA"))
                {
                    sql += " ma.name LIKE @keyword ";
                }
                else
                {
                    sql += " (mp.name LIKE @keyword OR ma.name LIKE @keyword) ";
                }
                keyword = keyword.Trim();
            }

            object[] paramsData = {
                                  new NpgsqlParameter("@keyword", DbType.String), '%' + keyword + '%'
                                  };

            return db.Scalar(sql, paramsData).AsInt();

        }
        public int GetCountT(string keyword = null, string filterData = null, int status = 0)
        {
            string SQL = "SELECT count(*) FROM master_tambon ";
            return db.Scalar(SQL).AsInt();
        }

        public Master GetProvince(int id)
        {
            string sql = "select id,name,lat,lon,status from master_province WHERE id = " + id;

            return db.Read(sql, MakeP).FirstOrDefault();
        }

        public int InsertProvince(Master model)
        {

            string sql = "INSERT INTO master_province (" +

                        " name," +
                        " lat," +
                        " lon," +
                        " status )" +
                       " VALUES (" +
                       " @name," +
                       " @lat," +
                       " @lon ,@status )";

            return db.Insert(sql, Take(model)).AsInt();
        }

        public int UpdateProvince(Master model)
        {
            string sql = " UPDATE master_province " +
                        " SET  name=@name, " +
                        "lat=@lat, " +
                        "lon=@lon , " +
                        "status=@status " +
                        " WHERE id =@id ";

            return db.Update(sql, Take(model)).AsInt();
        }

        public Master GetAmphur(int id)
        {
            string sql = "select id,pid,name,lat,lon,status from master_amphur WHERE id = " + id;

            return db.Read(sql, MakeA).FirstOrDefault();
        }

        public int InsertAmphur(Master model)
        {

            string sql = "INSERT INTO master_amphur (" +
                        " pid," +
                        " name," +
                        " lat," +
                        " lon," +
                        " status )" +
                       " VALUES (" +
                       " @pid," +
                       " @name," +
                       " @lat ," +
                       " @lon ,@status )";

            return db.Insert(sql, Take(model)).AsInt();
        }

        public int UpdateAmphur(Master model)
        {
            string sql = " UPDATE master_amphur " +
                        " SET  pid=@pid, " +
                        "name=@name, " +
                        "lat=@lat, " +
                        "lon=@lon , " +
                        "status=@status " +
                        " WHERE id =@id ";

            return db.Update(sql, Take(model)).AsInt();
        }



        static Func<IDataReader, Master> MakeListP = reader => new Master
        {
            Id = reader["id"].AsInt(),
            Name = reader["name"].AsString(),
            Lat = reader["lat"].AsDouble().ToString(),
            Lon = reader["lon"].AsDouble().ToString(),
            Status = reader["status"].AsInt()

        };

        static Func<IDataReader, Master> MakeP = reader => new Master
        {
            Id = reader["id"].AsInt(),
            Name = reader["name"].AsString(),
            Lat = reader["lat"].AsDouble().ToString(),
            Lon = reader["lon"].AsDouble().ToString(),
            Status = reader["status"].AsInt()

        };

        static Func<IDataReader, Master> MakeListA = reader => new Master
        {
            Id = reader["idA"].AsInt(),
            PID = reader["pid"].AsInt(),
            NameP = reader["nameP"].AsString(),
            Name = reader["nameA"].AsString(),
            Lat = reader["latA"].AsDouble().ToString(),
            Lon = reader["lonA"].AsDouble().ToString(),
            Status = reader["statusA"].AsInt()
          
        };

        static Func<IDataReader, Master> MakeA = reader => new Master
        {
            Id = reader["id"].AsInt(),
            PID = reader["pid"].AsInt(),
            Name = reader["name"].AsString(),
            Lat = reader["lat"].AsDouble().ToString(),
            Lon = reader["lon"].AsDouble().ToString(),
            Status = reader["status"].AsInt()

        };

        object[] Take(Master master)
        {
            return new object[]{
                new NpgsqlParameter("@id",DbType.Int32),master.Id,
                new NpgsqlParameter("@pid",DbType.Int32),master.PID,

                new NpgsqlParameter("@nameP",DbType.String),master.NameP,
                new NpgsqlParameter("@name",DbType.String),master.Name,
                new NpgsqlParameter("@lat",DbType.Double),Convert.ToDouble(master.Lat),                
                new NpgsqlParameter("@lon",DbType.Double),Convert.ToDouble(master.Lon),
                new NpgsqlParameter("@status",DbType.Int32),master.Status,
                
            };

        }
    }
}

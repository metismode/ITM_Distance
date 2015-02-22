using Distance.Business;
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

            sql += " ".OrderBy(sortExpresstion).Limit(limitExpression).Offset(offsetExpression);
            return db.Read(sql, MakeList).ToList();
        }
        public List<Master> GetAmphurList(string sortExpresstion = "\"id\" ASC", string limitExpression = "", string offsetExpression = "0", string keyword = null, string filterData = null, int status = 0)
        {
            string sql = "SELECT id, " +
                            "name, " +
                            "lat, " +
                            "lon, " +
                            "status " +
                            "FROM master_amphur ";

            sql += " ".OrderBy(sortExpresstion).Limit(limitExpression).Offset(offsetExpression);
            return db.Read(sql, MakeList).ToList();
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
            return db.Read(sql, MakeList).ToList();
        }

        public int GetCountP(string keyword = null, string filterData = null, int status = 0)
        {
            string SQL = "SELECT count(*) FROM master_province ";
            return db.Scalar(SQL).AsInt();
        }
        public int GetCountA(string keyword = null, string filterData = null, int status = 0)
        {
            string SQL = "SELECT count(*) FROM master_amphur ";
            return db.Scalar(SQL).AsInt();
        }
        public int GetCountT(string keyword = null, string filterData = null, int status = 0)
        {
            string SQL = "SELECT count(*) FROM master_tambon ";
            return db.Scalar(SQL).AsInt();
        }

        static Func<IDataReader, Master> MakeList = reader => new Master
        {
            Id = reader["id"].AsInt(),
            Name = reader["name"].AsString(),
            Lat = reader["lat"].AsString(),
            Lon = reader["lon"].AsString(),
            Status = reader["status"].AsInt()
          
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Configuration;
using Npgsql;

namespace Distance.Data.Npgsql
{
    public class Db
    {
        static DbProviderFactory factory = DbProviderFactories.GetFactory("Npgsql");
        public string connectionString { get; set; }

        private DbConnection _connection = null;
        private DbTransaction _transaction = null;

        public Db(string conn = "DefaultConnection")
        {
            if (conn == null)
                connectionString = ConfigurationManager.ConnectionStrings[1].ConnectionString;
            else
                connectionString = ConfigurationManager.ConnectionStrings[conn].ConnectionString;
        }

        public void BeginTransaction()
        {
            this._connection = CreateConnection();
            this._transaction = this._connection.BeginTransaction();
        }

        public void EndTransaction()
        {
            if (this._transaction != null)
            {
                this._transaction.Commit();
            }
            this._connection.Close();
            this._connection = null;
            this._transaction = null;
        }

        public void RollBack()
        {
            this._connection.Close();
            this._connection = null;
            this._transaction = null;
        }

        public IEnumerable<T> Read<T>(string sql, Func<IDataReader, T> make, params object[] parms)
        {

            if (this._connection == null)
            {
                using (var connection = CreateConnection())
                {
                    using (var command = CreateCommand(sql, connection, parms))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            List<T> result = new List<T>();

                            while (reader.Read())
                            {
                                result.Add(make(reader));
                            }

                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }

                            foreach (var data in result)
                            {
                                yield return data;
                            }
                        }
                    }
                }
            }
            else
            {
                using (var command = CreateCommand(sql, this._connection, parms))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<T> result = new List<T>();

                        while (reader.Read())
                        {
                            result.Add(make(reader));
                        }

                        if (_connection.State == ConnectionState.Open && this._transaction == null)
                        {
                            _connection.Close();
                        }

                        foreach (var data in result)
                        {
                            yield return data;
                        }
                    }
                }
            }
        }

        public object Scalar(string sql, params object[] parms)
        {
            if (this._connection == null)
            {
                using (var connection = CreateConnection())
                {
                    using (var command = CreateCommand(sql, connection, parms))
                    {
                        object returnObj = command.ExecuteScalar();
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        return returnObj;
                    }
                }
            }
            else
            {
                using (var command = CreateCommand(sql, this._connection, parms))
                {
                    object returnObj = command.ExecuteScalar();
                    if (_connection.State == ConnectionState.Open && this._transaction == null)
                    {
                        _connection.Close();
                    }
                    return returnObj;
                }
            }
        }

        public int Insert(string sql, params object[] parms)
        {
            if (this._connection == null)
            {
                using (var connection = CreateConnection())
                {
                    using (var command = CreateCommand(sql + ";SELECT lastval();", connection, parms))
                    {
                        //command.ExecuteScalar();
                        int result = int.Parse(command.ExecuteScalar().ToString());
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        return result;
                    }
                }
            }
            else
            {
                using (var command = CreateCommand(sql + ";SELECT lastval();", this._connection, parms))
                {
                    //command.ExecuteScalar();
                    int result = int.Parse(command.ExecuteScalar().ToString());
                    if (_connection.State == ConnectionState.Open && this._transaction == null)
                    {
                        _connection.Close();
                    }
                    return result;
                }
            }
        }

        public int Update(string sql, params object[] parms)
        {
            if (this._connection == null)
            {
                using (var connection = CreateConnection())
                {
                    using (var command = CreateCommand(sql, connection, parms))
                    {
                        int result = command.ExecuteNonQuery();
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        return result;
                    }
                }
            }
            else
            {
                using (var command = CreateCommand(sql, this._connection, parms))
                {
                    int result = command.ExecuteNonQuery();
                    if (_connection.State == ConnectionState.Open && this._transaction == null)
                    {
                        _connection.Close();
                    }
                    return result;
                }
            }

        }

        public int Delete(string sql, params object[] parms)
        {
            return Update(sql, parms);
        }

        DbConnection CreateConnection()
        {
            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }



            return connection;
        }

        DbCommand CreateCommand(string sql, DbConnection conn, params object[] parms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var command = factory.CreateCommand();
            command.Connection = conn;
            command.CommandText = sql;
            //command.Parameters.AddRange(parms);
            int index = 0;
            foreach (object o in parms)
            {
                if (o is NpgsqlParameter)
                {
                    command.Parameters.Add(o);
                }
                else
                {
                    command.Parameters[index].Value = o;
                    index++;
                }
            }

            return command;
        }

        DbDataAdapter CreateAdapter(DbCommand command)
        {
            var adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = command;
            return adapter;
        }
    }

    public static class DbExtensions
    {
        // adds parameters to a command object

        public static void AddParameters(this DbCommand command, object[] parms)
        {
            if (parms != null && parms.Length > 0)
            {

                // ** Iterator pattern

                // NOTE: processes a name/value pair at each iteration

                for (int i = 0; i < parms.Length; i += 2)
                {
                    string name = parms[i].ToString();

                    // no empty strings to the database

                    if (parms[i + 1] is string && (string)parms[i + 1] == "")
                        parms[i + 1] = null;

                    // if null, set to DbNull

                    object value = parms[i + 1] ?? DBNull.Value;

                    // ** Factory pattern

                    var dbParameter = command.CreateParameter();
                    dbParameter.ParameterName = name;
                    dbParameter.Value = value;

                    command.Parameters.Add(dbParameter);
                }
            }
        }
    }
}

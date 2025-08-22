using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BlogDataLibrary.Database
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        // query data (SELECT)
        public IEnumerable<T> LoadData<T, U>(
            string sql, U parameters, string connectionStringName)
        {
            using (IDbConnection connection =
                new SqlConnection(_config.GetConnectionString(connectionStringName)))
            {
                return connection.Query<T>(sql, parameters);
            }
        }

        // save data (INSERT, UPDATE, DELETE)
        public void SaveData<T>(
            string sql, T parameters, string connectionStringName)
        {
            using (IDbConnection connection =
                new SqlConnection(_config.GetConnectionString(connectionStringName)))
            {
                connection.Execute(sql, parameters);
            }
        }
    }
}
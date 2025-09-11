using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BlogDataLibrary.Database
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public IEnumerable<T> LoadData<T, U>(string sql, U parameters, string connectionStringName, bool isStoredProcedure = false)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>(sql, parameters,
                    commandType: isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text);
            }
        }

        public void SaveData<T>(string sql, T parameters, string connectionStringName, bool isStoredProcedure = false)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, parameters,
                    commandType: isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text);
            }
        }
    }
}
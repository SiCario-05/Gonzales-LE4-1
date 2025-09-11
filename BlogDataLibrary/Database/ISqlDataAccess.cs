using System.Collections.Generic;

namespace BlogDataLibrary.Database
{
    public interface ISqlDataAccess
    {
        IEnumerable<T> LoadData<T, U>(string sql, U parameters, string connectionStringName, bool isStoredProcedure = false);
        void SaveData<T>(string sql, T parameters, string connectionStringName, bool isStoredProcedure = false);
    }
}
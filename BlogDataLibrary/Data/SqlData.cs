using BlogDataLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDataLibrary.Data
{
    public class SqlData
    {
        private readonly ISqlDataAccess _db;
        private readonly string _connectionStringName = "Default";

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        // ===== Users =====
        public void InsertUser(string username, string firstName, string lastName, string password)
        {
            string sql = "INSERT INTO dbo.Users (UserName, FirstName, LastName, Password) " +
                         "VALUES (@UserName, @FirstName, @LastName, @Password)";
            _db.SaveData(sql, new { UserName = username, FirstName = firstName, LastName = lastName, Password = password }, _connectionStringName);
        }

        public IEnumerable<dynamic> GetAllUsers()
        {
            string sql = "SELECT Id, UserName, FirstName, LastName FROM dbo.Users";
            return _db.LoadData<dynamic, dynamic>(sql, new { }, _connectionStringName);
        }

        // ===== Posts =====
        public void InsertPost(int userId, string title, string body, DateTime dateCreated)
        {
            string sql = "INSERT INTO dbo.Posts (UserId, Title, Body, DateCreated) " +
                         "VALUES (@UserId, @Title, @Body, @DateCreated)";
            _db.SaveData(sql, new { UserId = userId, Title = title, Body = body, DateCreated = dateCreated }, _connectionStringName);
        }

        public IEnumerable<ListPostModel> GetAllPosts()
        {
            string sql = "SELECT p.Id, p.Title, u.UserName, p.DateCreated " +
                         "FROM dbo.Posts p INNER JOIN dbo.Users u ON p.UserId = u.Id";
            return _db.LoadData<ListPostModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public ListPostModel GetPostById(int postId)
        {
            string sql = "SELECT p.Id, p.Title, u.UserName, p.DateCreated " +
                         "FROM dbo.Posts p INNER JOIN dbo.Users u ON p.UserId = u.Id " +
                         "WHERE p.Id = @Id";
            var result = _db.LoadData<ListPostModel, dynamic>(sql, new { Id = postId }, _connectionStringName);
            return result != null ? System.Linq.Enumerable.FirstOrDefault(result) : null;
        }
    }
}

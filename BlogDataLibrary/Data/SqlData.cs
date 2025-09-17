using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlogDataLibrary.Data
{
    public class SqlData : ISqlData
    {
        private readonly ISqlDataAccess _db;
        private const string connectionStringName = "SqlDb";

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        public UserModel Authenticate(string username, string password)
        {
            return _db.LoadData<UserModel, dynamic>("dbo.spUsers_Authenticate",
                new { username, password },
                connectionStringName,
                isStoredProcedure: true)
                .FirstOrDefault();
        }

        public void Register(string username, string firstName, string lastName, string password)
        {
            _db.SaveData("dbo.spUsers_Register",
                new { username, firstName, lastName, password },
                connectionStringName,
                isStoredProcedure: true);
        }

        public void AddPost(PostModel post)
        {
            _db.SaveData("dbo.spPosts_Insert",
                new { post.UserId, post.Title, post.Body, post.DateCreated },
                connectionStringName,
                isStoredProcedure: true);
        }

        public List<ListPostModel> ListPosts()
        {
            return _db.LoadData<ListPostModel, dynamic>("dbo.spPosts_List",
                new { },
                connectionStringName,
                isStoredProcedure: true)
                .ToList();
        }

        public ListPostModel ShowPostDetails(int id)
        {
            return _db.LoadData<ListPostModel, dynamic>("dbo.spPosts_Details",
                new { id },
                connectionStringName,
                isStoredProcedure: true)
                .FirstOrDefault();
        }
    }
}
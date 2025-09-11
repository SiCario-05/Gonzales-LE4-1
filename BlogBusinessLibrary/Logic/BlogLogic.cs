using BlogDataLibrary.Data;
using System;
using System.Collections.Generic;

namespace BlogBusinessLibrary.Logic
{
    public class BlogLogic
    {
        private readonly SqlData _db;

        public BlogLogic(SqlData db)
        {
            _db = db;
        }

        // ===== Users =====
        public void CreateUser(string username, string firstName, string lastName, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new Exception("Username and password are required.");

            _db.InsertUser(username, firstName, lastName, password);
        }

        public IEnumerable<dynamic> GetUsers()
        {
            return _db.GetAllUsers();
        }

        public dynamic Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            return _db.Authenticate(username, password);
        }
    }
}
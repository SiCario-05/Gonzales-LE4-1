using BlogDataLibrary.Data;
using BlogDataLibrary.Models;
using System;
using System.Collections.Generic;

namespace BlogBusinessLibrary
{
    public class UserProcessor
    {
        private readonly SqlData _data;

        public UserProcessor(SqlData data)
        {
            _data = data;
        }

        public void CreateUser(string username, string firstName, string lastName, string password)
        {
            _data.InsertUser(username, firstName, lastName, password);
            Console.WriteLine($"✅ User '{username}' created.");
        }

        public IEnumerable<UserModel> GetUsers()
        {
            return _data.GetAllUsers();
        }

        public UserModel AuthenticateUser(string username, string password)
        {
            var user = _data.Authenticate(username, password);

            if (user != null)
                Console.WriteLine($"✅ Login success: {user.UserName} ({user.FirstName} {user.LastName})");
            else
                Console.WriteLine("❌ Login failed.");

            return user;
        }
    }
}
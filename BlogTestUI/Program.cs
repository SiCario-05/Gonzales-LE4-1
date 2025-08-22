using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogDataLibrary.Database;
using BlogDataLibrary.Data;

namespace BlogTestUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // build configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // create SqlDataAccess and SqlData
            ISqlDataAccess db = new SqlDataAccess(config);
            SqlData sqlData = new SqlData(db);

            // insert a new user
            sqlData.InsertUser("jdoe", "John", "Doe", "123456");

            // list all users
            var users = sqlData.GetAllUsers();
            Console.WriteLine("Users:");
            foreach (var u in users)
            {
                Console.WriteLine($"{u.Id} | {u.UserName} | {u.FirstName} {u.LastName}");
            }

            // insert a post
            sqlData.InsertPost(1, "Hello World", "This is my first post.", DateTime.Now);

            // list all posts
            var posts = sqlData.GetAllPosts();
            Console.WriteLine("\nPosts:");
            foreach (var p in posts)
            {
                Console.WriteLine($"{p.Id} | {p.Title} by {p.UserName} on {p.DateCreated}");
            }

            // get a post by Id
            var post = sqlData.GetPostById(1);
            if (post != null)
                Console.WriteLine($"\nPost Id 1: {post.Title} by {post.UserName}");

            Console.ReadLine();
        }
    }
}
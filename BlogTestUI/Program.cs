using BlogDataLibrary.Data;
using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace BlogTestUI
{
    internal class Program
    {
        static SqlData GetConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration config = builder.Build();
            ISqlDataAccess dbAccess = new SqlDataAccess(config);
            SqlData db = new SqlData(dbAccess);

            return db;
        }

        private static UserModel GetCurrentUser(SqlData db)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            UserModel user = db.Authenticate(username, password);
            return user;
        }

        public static void Authenticate(SqlData db)
        {
            UserModel user = GetCurrentUser(db);
            if (user == null)
            {
                Console.WriteLine("Invalid credentials.");
            }
            else
            {
                Console.WriteLine($"Welcome, {user.FirstName} {user.LastName}");
            }
        }

        public static void Register(SqlData db)
        {
            Console.Write("Enter new username: ");
            var username = Console.ReadLine();

            Console.Write("Enter first name: ");
            var firstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            var lastName = Console.ReadLine();

            Console.Write("Enter new password: ");
            var password = Console.ReadLine();

            db.Register(username, firstName, lastName, password);
            Console.WriteLine("Registration successful!");
        }

        private static void AddPost(SqlData db)
        {
            Console.WriteLine("Please log in to add a post.");
            UserModel user = GetCurrentUser(db);

            if (user == null)
            {
                Console.WriteLine("Invalid credentials. Cannot create a post.");
                return;
            }

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Write body: ");
            string body = Console.ReadLine();

            PostModel post = new PostModel
            {
                Title = title,
                Body = body,
                DateCreated = DateTime.Now,
                UserId = user.Id
            };

            db.AddPost(post);
            Console.WriteLine("Post created successfully!");
        }

        private static void ListPosts(SqlData db)
        {
            List<ListPostModel> posts = db.ListPosts();

            if (posts.Count == 0)
            {
                Console.WriteLine("No posts found.");
                return;
            }

            Console.WriteLine("--- All Posts ---");
            foreach (ListPostModel post in posts)
            {
                Console.WriteLine($"{post.Id}. Title: {post.Title} by {post.UserName} [{post.DateCreated:yyyy-MM-dd}]");
                string bodyPreview = post.Body.Length > 20 ? post.Body.Substring(0, 20) : post.Body;
                Console.WriteLine($"   {bodyPreview}...");
                Console.WriteLine();
            }
        }

        private static void ShowPostDetails(SqlData db)
        {
            Console.Write("Enter a post ID to see details: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                ListPostModel post = db.ShowPostDetails(id);

                if (post == null)
                {
                    Console.WriteLine("Post not found.");
                    return;
                }

                Console.WriteLine($"\n--- Post Details: {post.Title} ---");
                Console.WriteLine($"by {post.FirstName} {post.LastName} [@{post.UserName}]");
                Console.WriteLine($"Published on {post.DateCreated:MMM d, yyyy}");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(post.Body);
                Console.WriteLine("-----------------------------------");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        static void Main(string[] args)
        {
            SqlData db = GetConnection();

            // Uncomment ONE function at a time to test it.

             Register(db);
             Authenticate(db);
             AddPost(db);
             ListPosts(db);
             ShowPostDetails(db);

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }
    }
}
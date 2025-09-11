// The fully corrected PostProcessor.cs file

using BlogDataLibrary.Data;
using BlogDataLibrary.Models; // <-- Make sure this using statement is correct
using System;
using System.Collections.Generic;

namespace BlogBusinessLibrary
{
    public class PostProcessor
    {
        private readonly SqlData _data;

        public PostProcessor(SqlData data)
        {
            _data = data;
        }

        public void CreatePost(int userId, string title, string body, DateTime dateCreated)
        {
            _data.InsertPost(userId, title, body, dateCreated);
        }

        // Now that the namespaces match, no cast is needed.
        public IEnumerable<ListPostModel> GetPosts()
        {
            return _data.GetAllPosts();
        }

        public PostModel GetPostById(int id)
        {
            return _data.GetPostById(id);
        }
    }
}
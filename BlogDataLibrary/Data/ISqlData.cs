using BlogDataLibrary.Models;
using System.Collections.Generic;

namespace BlogDataLibrary.Data
{
    public interface ISqlData
    {
        UserModel Authenticate(string username, string password);
        void Register(string username, string firstName, string lastName, string password);
        List<ListPostModel> ListPosts();
        ListPostModel ShowPostDetails(int id);
        void AddPost(PostModel post);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDataLibrary
{
    public class ListPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

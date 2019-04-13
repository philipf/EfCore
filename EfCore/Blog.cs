using System.Collections.Generic;

namespace EfCore
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        private readonly ICollection<Post> _posts;  // Define a private backing field
        public IEnumerable<Post> Posts => _posts;   // Only expose a getter

        public Blog()
        {
            _posts = new List<Post>();
        }

        public void AddPost(Post p)
        {
            _posts.Add(p);
        }
    }
}
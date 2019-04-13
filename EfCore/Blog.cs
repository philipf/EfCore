using System.Collections.Generic;

namespace EfCore
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        private readonly ICollection<Post> _posts;

        public IEnumerable<Post> Posts => _posts;

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
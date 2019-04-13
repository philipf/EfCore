using System;
using Microsoft.EntityFrameworkCore;

namespace EfCore
{
    static class Program
    {
        static void Main()
        {
            using (var db = new MyDbContext())
            {
                SaveToDb(db);
            }

            using (var db = new MyDbContext())
            {
                RetrieveFromDb(db);
            }

            Console.ReadLine();
        }

        private static void RetrieveFromDb(MyDbContext db)
        {
            foreach (var blog in db.Blogs.Include(b => b.Posts))
            {
                Console.WriteLine($" - {blog.Url}");
                foreach (var post in blog.Posts)
                {
                    Console.WriteLine($"     -> { post.Title}");
                }
            }
        }

        private static void SaveToDb(MyDbContext db)
        {
            var blog = new Blog {Url = "http://blogs.msdn.com/adonet"};
            db.Blogs.Add(blog);
            blog.AddPost(new Post {Blog = blog, Title = "Sample content"});

            var count = db.SaveChanges();
            Console.WriteLine("{0} records saved to database", count);
            Console.WriteLine();
            Console.WriteLine("All blogs in database:");
        }
    }
}
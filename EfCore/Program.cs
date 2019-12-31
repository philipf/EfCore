using System;
using System.Linq;
using System.Threading;

namespace EfCore
{
    static class Program
    {
        static void Main()
        {
            using (var db = new MyDbContext())
            {
                for (int i = 2; i < 11; i++)
                {
                    InsertBlog(db, i);    
                }
                

                //var now = DateTime.UtcNow;

                //var result = db.Blogs.Where(b => b.StartDate <= now && b.EndDate <= now).ToList();

                Console.WriteLine();
                Console.WriteLine("All blogs in database:");

            }
        }

        private static void InsertBlog(MyDbContext db, int i)
        {
            var blog = new Blog {Url = "http://blogs.msdn.com/adonet/" + i};
            blog.StartDate = new DateTime(2019, 12, 24, 0, 0, 0);
            blog.EndDate = new DateTime(2019, 12, 24, 23, 59, 59);

            db.Blogs.Add(blog);
            var count = db.SaveChanges();
            Console.WriteLine("{0} records saved to database", count);
        }
    }
}
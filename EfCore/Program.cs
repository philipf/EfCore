using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;

namespace EfCore
{
    static class Program
    {
        static void Main()
        {
            using (var db = new MyDbContext())
            {
                if (!db.ProductNodes.Any())
                    AddSampleData(db);

                Console.WriteLine("Starting...");

                var p2 = db.ProductNodes
                    .Include(n => n.Level)
                    .Include(n => n.Sections)
                //    //.Include(n => n.Parent)
                //    .Include(n => n.Parent.Sections)
                //    .Include(n => n.Parent.Parent.Sections)
                //    .Include(n => n.Parent.Parent.Parent.Sections)
                    .Single(o => o.Id == 3);

                //IQueryable<ProductNode> q = db.ProductNodes.AsQueryable();

                //for (int i = 0; i < 3; i++)
                //{
                //    q = q.Include(n => n.Level);
                //    q = q.Include(n => n.Sections);
                //    q = q.Include(p => p.Parent.Sections);
                //    q = q.Include(p => p.Parent.Level);
                //    q = q.Include(p => p.Parent);


                //    IIncludableQueryable<ProductNode, ProductNode> x = q.Include(p => p.Parent)
                //        .ThenInclude(p => p.Parent);


                //}


                //var p2 = q.Single(p => p.Id == 3);



                //var baseSections = db.Entry(p2)
                //    .Collection(s => s.Sections)
                //    .Query()
                //    .Where(s => s is BasicInfoSection || s is TermsSection)
                //    .ToList();


                Console.WriteLine(p2.Get<BasicInfoSection>().Code);
                Console.WriteLine(p2.Get<TermsSection>().MinTerms);


                Console.WriteLine("Done");
            }
        }

        private static void AddSampleData(MyDbContext db)
        {
            var productTypeLevel = new ProductLevel("PT", "Product Type");
            var productLevel     = new ProductLevel("P", "Product");
            var campaignLevel    = new ProductLevel("C", "Campaign");

            db.ProductLevels.Add(productTypeLevel);
            db.ProductLevels.Add(productLevel);
            db.ProductLevels.Add(campaignLevel);
            db.SaveChanges();

            // Create product hierarchy
            var pt1 = ProductNodeFactory.Create(productTypeLevel);

            {
                var bs = pt1.Get<BasicInfoSection>();
                bs.Code = "PT1";

                var ts = pt1.Get<TermsSection>(false);
                ts.MinTerms = 12;
            }

            var p1 = ProductNodeFactory.Create(productLevel);
            {
                pt1.AddNode(p1);

                var bs = p1.Get<BasicInfoSection>(false);
                bs.Code = "P1";

                var ts = p1.Get<TermsSection>(false);
                ts.MinTerms = 32;
            }

            var c1 = ProductNodeFactory.Create(campaignLevel);
            {
                p1.AddNode(c1);

                var bs = c1.Get<BasicInfoSection>(false);
                bs.Code = "C1";

                var ts = c1.Get<TermsSection>(false);
                ts.MinTerms = 48;
            }

            db.ProductNodes.Add(pt1);
            db.SaveChanges();
        }
    }
}
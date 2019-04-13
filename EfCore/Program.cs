using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EfCore
{
    static class Program
    {
        static void Main()
        {
            using (var db = new MyDbContext())
            {
                //if (!db.ProductNodes.Any())
                //    AddSampleData(db);

                Console.WriteLine("Starting...");

                //ProductNode p2 = db.ProductNodes
                //    .Include(p => p.Sections)
                //    .Include(p => (BasicInfoSection) p.Parent.Sections)
                //        //.ThenInclude(p => p.Sections)
                //    .Single(p => p.Id == 2);

                var p2 = db.Set<ProductNode>()
                    .Where(o => o.Id == 2)
                    .Include(o => (TermsSection) o.Sections)
                    .Single();

                //db.Entry(p2).Collection(e => e.Sections)
                //    .Query().OfType<BasicInfoSection>()
                //    //.Include(e => e.OffshoreAccounts)
                //    //.ThenInclude(e => e.AccountInfo)
                //    .Load();


                Console.WriteLine(p2.Get<BasicInfoSection>().Code);
                Console.WriteLine(p2.Get<TermsSection>(true).MinTerms);


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

                var ts = pt1.Get<TermsSection>();
                ts.MinTerms = 12;
            }

            var p1 = ProductNodeFactory.Create(productLevel);
            pt1.AddNode(p1);

            {
                var bs = p1.Get<BasicInfoSection>();
                bs.Code = "P1";

                var ts = p1.Get<TermsSection>();
                ts.MinTerms = 32;
            }

            pt1.Get<TermsSection>();
            p1.Get<TermsSection>(true);

            db.ProductNodes.Add(pt1);
            db.SaveChanges();
        }
    }
}
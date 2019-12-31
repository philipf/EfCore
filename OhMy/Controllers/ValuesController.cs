using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace OhMy.Controllers
{
    [Produces("application/json")]
    public class BlogController : ODataController
    {
        // GET api/values
        [EnableQuery]
        public IQueryable<Blog> Get()
        {
            var ctx = new MyDbContext();
            return ctx.Blogs;
            //return new string[] { "value1", "value2" };
        }
    }
}

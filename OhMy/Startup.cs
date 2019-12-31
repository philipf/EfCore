using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;
using Microsoft.OData;
using Microsoft.AspNet.OData.Formatter;

namespace OhMy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddOData();
            services.AddTransient<BlogModelBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BlogModelBuilder modelBuilder)
        {
            var builder = new ODataConventionModelBuilder(app.ApplicationServices);

            builder.EntitySet<Blog>("Blogs");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMvc();

            app.UseMvc(routeBuilder => {

                //routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
                routeBuilder.MapODataServiceRoute("ODataRoutes", "odata", modelBuilder.GetEdmModel(app.ApplicationServices));
                routeBuilder.EnableDependencyInjection();

                routeBuilder.Expand().Select().OrderBy().Filter();


            });
        }

        public class BlogModelBuilder
        {
            public IEdmModel GetEdmModel(IServiceProvider serviceProvider)
            {
                var builder = new ODataConventionModelBuilder(serviceProvider);
                builder.EntitySet<Blog>(nameof(Blog))
                    .EntityType
                    .Filter() // Allow for the $filter Command
                    .Count() // Allow for the $count Command
                    .Expand() // Allow for the $expand Command
                    .OrderBy() // Allow for the $orderby Command
                    .Page() // Allow for the $top and $skip Commands
                    .Select() // Allow for the $select Command; 
                    .ContainsMany(x => x.Posts);

                builder.EntitySet<Post>(nameof(Post))
                    .EntityType
                    .Filter() // Allow for the $filter Command
                    .Count() // Allow for the $count Command
                    .Expand() // Allow for the $expand Command
                    .OrderBy() // Allow for the $orderby Command
                    .Page() // Allow for the $top and $skip Commands
                    .Select() // Allow for the $select Command
                    .Expand(); 
              
                return builder.GetEdmModel();
            }
        }
    }
}

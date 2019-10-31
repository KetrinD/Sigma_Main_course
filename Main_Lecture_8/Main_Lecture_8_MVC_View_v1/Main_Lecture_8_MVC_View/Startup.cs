using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Api.Demo.Controllers;

namespace Web.Api.Demo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940


        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<Repository>();
            services.Configure<RepositoryOptions>(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
            //app.UseMvc(builder => builder.MapRoute("defaultRoute",
            //    "api/{controller=demo}/{action=gethello}"
            //));

            //var customRouteHandler = new RouteHandler(context =>
            //{
            //    var routeValues = context.GetRouteData().Values;
            //    return context.Response.WriteAsync($"your route data: {string.Join(", ", routeValues)}");
            //});
            //var routeBuilder = new RouteBuilder(app, customRouteHandler);
            //routeBuilder.MapGet("customRouter/{name}",

            //    context =>
            //    {
            //        var name = context.GetRouteValue("name");
            //        return context.Response.WriteAsync($"Hi, {name}");
            //    }
            //);


            //var router = routeBuilder.Build();
            //app.UseRouter(router);

        }
    }
}

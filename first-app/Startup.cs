using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace first_app
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IRestaurantData, InMemoryRestaurantData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            

            // // will get routed to Home/Index
            // app.UseMvcWithDefaultRoute()

            // app.UseMvc(
            //     routes => routes.MapRoute(
            //         "default",
            //         "{controller=home}/{action=Index}/{id?}"
            //     )
            // );
            app.UseMvc(RouteConfig);

            app.UseDefaultFiles();
            app.UseStaticFiles();            

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Hello World running in {env.EnvironmentName}");
            });
        }

        private void RouteConfig(IRouteBuilder router)
        {
            router.MapRoute(
                "Default",
                "{controller=Home}/{action=Index}/{id?}"
            );
        }
    }
}

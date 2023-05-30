using IssueStock.Demo.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueStock.Demo
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
            services.AddRazorPages();

            // Adding static resources. Only for development and demo purposes only. can use in prod if the configs rarly change with time
            services.AddIdentityServer()
                    .AddInMemoryClients(IdentityConfiguration.Clients)
                    .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
                    .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
                    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                    .AddTestUsers(IdentityConfiguration.TestUsers)
                    .AddDeveloperSigningCredential();  // for development purposes 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Stock.Demo Identity Server Works!");
                });
            });
        }
    }
}

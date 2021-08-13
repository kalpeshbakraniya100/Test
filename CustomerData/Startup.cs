using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Data.Infrastructure;
using System.Data;
using Data.Repositories;
using System.Text.Json;
using System.Data.SqlClient;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace CustomerData
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
            string dbConnectionString = this.Configuration.GetConnectionString("DefaultConnection");
            string db100ConnectionString = this.Configuration.GetConnectionString("Server100DefaultConnection");
            string dblocalConnectionString = this.Configuration.GetConnectionString("LocalDefaultConnection");
            var connectionDict = new Dictionary<DbConnectionString, string>
            {
                { DbConnectionString.Connection100, dbConnectionString },
                { DbConnectionString.Connection163, db100ConnectionString},
                { DbConnectionString.ConnectionLocal, db100ConnectionString},
                {DbConnectionString.LocaDb, dblocalConnectionString }
            };
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(dbConnectionString));            
            services.AddSingleton<IDictionary<DbConnectionString, string>>(connectionDict);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<ICustomersRepository, CustomersRepository>();
            //services.AddSingleton<IRepository<Customers>, CustomersRepository>();
            services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

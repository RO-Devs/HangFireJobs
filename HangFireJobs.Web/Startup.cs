using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;
using HangFireJobs.Web.Context;
using HangFireJobs.Web.Extensions;
using HangFireJobs.Web.Services;
using HangFireJobs.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HangFireJobs.Web
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
            services.AddDbContext<JobContext>
                (opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("AdventureWorks")));
            services.AddTransient<IEmployeeService, EmployeeService>();

            services.ConfigureHangFire(Configuration);

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
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
            app.UseHangfireDashboard();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            var employeeService = serviceProvider.GetService<IEmployeeService>();

            recurringJobManager.AddOrUpdate("Get_All_Employees", () => employeeService.PrintAllEmployeeTitles(), Cron.Minutely);
            recurringJobManager.AddOrUpdate("Disable_All_Employees", () => employeeService.DisableAllEmployees(), Cron.Minutely);
        }





    }
}

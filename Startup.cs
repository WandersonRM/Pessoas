
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Pessoas.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
using Pessoas.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;

namespace Pessoas
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
           
            Configuration = configuration;
            _env = environment.EnvironmentName;
        }

        public IConfiguration Configuration { get; }
        private string _env { get; }
    
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

          

            if(_env == "Development") {
                var builder = new System.Data.SqlClient.SqlConnectionStringBuilder(Configuration["App:ConnectionString"]);

                services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(builder.ConnectionString));
            }
            else if(_env == "Production")
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                      options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStringPessoas", EnvironmentVariableTarget.Machine)));
            }


             services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

         

            services.AddControllersWithViews();
            services.AddRazorPages();

            //configura o uso da Sess�o
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
      
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else if(env.IsProduction() )
            {
                app.UseExceptionHandler("/Error");
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();


                
                endpoints.MapControllerRoute(
                    name: "EnderecosUsuario",
                    pattern: "{controller=Pessoas}/{action=details}/{id?}"
                    );
                endpoints.MapRazorPages();


            });
        }
    }
}


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

            /*services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            string conexaoBD = "AI1sFiayytywNPZu3oAyxKlDoSHOf5IjSuplKImu0DiS8UE7vl8t3AMtW6lrIT627n2yCn1uh7okj6EuUQCSYc+2EhzbevqFwyew1ezwnUzwlPobPlQB4K/GFeeM9wsNF6mNtwZT/hAd4DA3g20ezOSW0WffY9EelNymUvs0m/G/ShbOSa5/1fuGU2fy2MxTsFJVrpRfvJROrpH+TJkybmu350xNp29FdAKB/9ozHRM=";
            var builder = new SqlConnectionStringBuilder(SecurityController.Decrypt(conexaoBD, _env));
            */
            var builder = new SqlConnectionStringBuilder(Configuration["App:ConnectionString"]);
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer( builder.ConnectionString ));
            
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            //configura o uso da Sessão
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

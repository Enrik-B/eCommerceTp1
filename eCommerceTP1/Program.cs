using eCommerceTP1.Models;
using eCommerceTP1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace eCommerceTP1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
            builder.Services.AddSession();
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<PanierService>();
            builder.Services.AddScoped<FactureService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<ProduitService>();
            builder.Services.AddDbContext<eCommerceTP1DbContext>(
                options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
                )
             );

            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            // Routes
            app.UseMvc(routes => routes.MapRoute("Default", "{controller=Home}/{action=Index}"));
            app.Run();
        }
    }
}

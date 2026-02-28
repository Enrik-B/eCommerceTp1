using eCommerceTP1.Services;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTP1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

            builder.Services.AddScoped<PanierService>();
            builder.Services.AddDbContext<eCommerceTP1DbContext>(
                options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
                )
             );

            var app = builder.Build();
            // Routes
            app.UseMvc(routes => routes.MapRoute("Default", "{controller=Home}/{action=Index}"));
            app.Run();
        }
    }
}

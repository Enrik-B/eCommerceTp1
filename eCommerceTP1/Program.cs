namespace eCommerceTP1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMvc(option => option.EnableEndpointRouting = false);     

            var app = builder.Build();
            // Routes
            app.UseMvc(routes => routes.MapRoute("Default", "{controller=Home}/{action=Index}"));
            app.Run();
        }
    }
}

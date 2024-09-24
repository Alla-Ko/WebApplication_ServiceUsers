
using WebApplication_Services.Services;

namespace WebApplication_Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IServiceUsers, ServiceUsers>();
            builder.Services.AddSingleton<UserContext>(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("MongoDbConnection");
                return new UserContext(connectionString);
            });
            builder.Services.AddControllers();
            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();
            app.UseHsts();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Frontend");
            });
            app.Run();
        }
    }
}

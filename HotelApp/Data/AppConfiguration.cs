using HotelApp.Controllers;
using HotelApp.Controllers.MenuControllers;
using HotelApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelApp.Data
{
    public static class AppConfiguration
    {
        public static IConfiguration BuildConfiguration() => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        public static ServiceProvider BuildServiceProvider(IConfiguration config)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(config);
            services.AddDbContext<ApplicationDbContext>
                (opts => opts.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<DataInitializer>();
            services.AddScoped<BookingService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<RoomService>();
            services.AddScoped<BookingMenu>();
            services.AddScoped<MainMenu>();
            services.AddScoped<CustomerMenu>();
            services.AddScoped<RoomMenu>();
            services.AddScoped<CustomerController>();
            services.AddScoped<RoomController>();
            services.AddScoped<BookingController>();

            return services.BuildServiceProvider();
        }
    }
}

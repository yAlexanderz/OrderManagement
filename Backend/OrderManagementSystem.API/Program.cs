using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderManagementSystem.Infrastructure.Persistence;
using OrderManagementSystem.Infrastructure.Repositories;
using OrderManagementSystem.Application.Interfaces;
using OrderManagementSystem.Application.Mappings;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Application.Features.Orders.Handlers;

namespace OrderManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://0.0.0.0:7107", "https://0.0.0.0:5106");
                });
    }

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));

            services.AddScoped<IDbConnection>(sp =>
            {
                var connectionString = Configuration.GetConnectionString("SqlServerConnection");
                return new SqlConnection(connectionString);
            });

            services.AddSingleton(sp => new MongoDbConfig(
                Configuration.GetConnectionString("MongoDbConnection"),
                Configuration["MongoDbDatabaseName"]));

            services.AddScoped<ICreateOrderCommandHandler, CreateOrderCommandHandler>();
            services.AddScoped<IGetOrdersQueryHandler, GetOrdersQueryHandler>();


            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();

            services.AddAutoMapper(typeof(OrderProfile));

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            services.AddCors(options =>
            {
                options.AddPolicy("DevelopmentPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Management API v1"));

                app.UseCors("DevelopmentPolicy");
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

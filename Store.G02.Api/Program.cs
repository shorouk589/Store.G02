
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using ServicesAbstraction;
using System.Threading.Tasks;
using AssemblyMapping = Services.AssemblyReference;
namespace Store.G02.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                   );
            });
            builder.Services.AddScoped<IDbInitializer, DbInitializer>(); // Allow DI For DbInitializer
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<IProductService, ProductService>();


            builder.Services.AddAutoMapper(typeof(AssemblyMapping).Assembly);



            var app = builder.Build();
            // Code

            #region Seeding


            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();// Ask CLR Create Objects
            await dbInitializer.InitializeAsync();

            #endregion



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

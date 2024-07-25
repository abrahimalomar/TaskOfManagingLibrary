
using Domain.Interfaces;
using Infrastructures.Data;
using Infrastructures.Data.Seed;
using Infrastructures.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace TaskOfManagingLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            Log.Logger = new LoggerConfiguration()
         .MinimumLevel.Information()
         .WriteTo.Console()
         .WriteTo.File("C:/Users/30698/source/applog.txt", rollingInterval: RollingInterval.Day)
         .CreateLogger();
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200", "http://127.0.0.1:5500") // Remove the trailing slash
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                var xmlfilePath = Path.Combine(AppContext.BaseDirectory, "TaskAPI.xml");
                option.IncludeXmlComments(xmlfilePath);
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
           

            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

            builder.Host.UseSerilog();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Use CORS
            app.UseCors("AllowSpecificOrigins"); 


            app.UseAuthorization();
           
            app.MapControllers();
        

            // Seed database
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                 DbInitializer.Initialize(context);
            }
            app.Run();
        }
    }
}

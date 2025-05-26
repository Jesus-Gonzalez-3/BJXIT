
using Inventarios_Controller.Services;
using InventariosModel.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventarios_Controller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<InventariosContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Inventarios")));
            ConstantsAPI? constants = builder.Configuration.GetSection("API").Get<ConstantsAPI>();
            builder.Services.AddSingleton<ConstantsAPI>(constants);

            builder.Services.AddScoped<UserServices>();
            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();
            app.UseCors(x => x.AllowAnyMethod()
                           .AllowAnyOrigin()
                           .AllowAnyHeader());


            app.MapControllers();

            app.Run();
        }
    }
}

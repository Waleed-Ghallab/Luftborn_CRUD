using Microsoft.EntityFrameworkCore;
using Luftborn_CRUD.Models;
//using Microsoft.Identity.Client;

namespace Luftborn_CRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            string txt = "";
            builder.Services.AddCors(op =>
            {
                op.AddPolicy(txt, op =>
                {
                    op.AllowAnyOrigin();  //to allow ajax requests from any outer domain
                    op.AllowAnyHeader();
                    op.AllowAnyMethod();
                });
            });

            //dependency injection using lazyloading as default
            builder.Services.AddDbContext<Luftborn_dbcontext>(op =>{
                op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("dbconnection"));
            });

            //ignore reference loop while serializing objects => max. 3levels
            builder.Services.AddControllers().AddNewtonsoftJson(op =>
            {
                op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //middlewares
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors(txt); //allow cross domain request

            app.Run();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Workshop.Infrastructure.Contracts.Context;
using Workshop.Application.Contracts;
using Workshop.Application.Impl;
using Workshop.Infrastructure.Contracts;
using Workshop.Infrastructure.Impl;

namespace Workshop.Presentation.WebAPI
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

			builder.Services.AddScoped<IVehicleService, VehicleService>();
			builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
			builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

			builder.Services.AddDbContext<WorkshopContext>(options => options.UseSqlServer(builder.Configuration["DefaultConfiguration"]));

			var app = builder.Build();

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

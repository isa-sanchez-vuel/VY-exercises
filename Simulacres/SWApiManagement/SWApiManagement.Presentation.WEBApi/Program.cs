using Microsoft.EntityFrameworkCore;
using SWApiManagement.Application.Contracts;
using SWApiManagement.Application.Impl;
using SWApiManagement.Infrastructure.Contracts;
using SWApiManagement.Infrastructure.Impl;
using SWApiManagement.Infrastructure.Context;
using SWApiManagement.XCutting;

namespace SWApiManagement.Presentation.WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddScoped<IPlanetService, PlanetService>();
			builder.Services.AddScoped<IPlanetsRepository, PlanetsRepository>();
			builder.Services.AddScoped<IApiImporter, ApiImporter>();

			builder.Services.AddDbContext<SWDBPlanetsContext>(options => options.UseSqlServer(builder.Configuration[GlobalVariables.DB_CONNECTION_STRING]));

			builder.Services.AddControllers();
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

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}

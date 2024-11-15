using Microsoft.EntityFrameworkCore;
using UniversitiesManagement.Application.Contracts;
using UniversitiesManagement.Application.Impl;
using UniversitiesManagement.Infrastructure.Context;
using UniversitiesManagement.Infrastructure.Contracts;
using UniversitiesManagement.Infrastructure.Impl;

namespace UniversitiesManagement.Presentation.WebApi
{
	public class Program
	{
		const string DB_STRING_CONFIG = "ConnectionStrings:DbConnection";

		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddScoped<IUniversityService, UniversityService>();
			builder.Services.AddScoped<IApiRepository, ApiRepository>();
			builder.Services.AddScoped<IDbUniversityRepository, DbUniversityRepository>();
			builder.Services.AddScoped<IDbWebpageRepository, DbWebpageRepository>();

			builder.Services.AddDbContext<UniversitiesManagementContext>(
				options => options.UseSqlServer(builder.Configuration[DB_STRING_CONFIG]
				));

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

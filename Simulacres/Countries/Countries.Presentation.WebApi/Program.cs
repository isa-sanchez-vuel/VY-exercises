using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Countries.Presentation.WebApi.Data;
using Countries.Infrastructure.Contracts;
using Countries.Infrastructure.Impl;
using Countries.Application.Contracts;
using Countries.Application.Impl;
using Countries.Infrastructure.Impl.Context;
namespace Countries.Presentation.WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddDbContext<CountriesPresentationWebApiContext>(options =>
			    options.UseSqlServer(builder.Configuration.GetConnectionString("CountriesPresentationWebApiContext") ?? throw new InvalidOperationException("Connection string 'CountriesPresentationWebApiContext' not found.")));

			// Add services to the container.
			builder.Services.AddScoped<IApiImporter, ApiImporter>();
			builder.Services.AddScoped<ICountryRepository, CountryRepository>();
			builder.Services.AddScoped<ICountryService, CountryService>();

			builder.Services.AddDbContext<CountryPopulationContext>(
				options => options.UseSqlServer(builder.Configuration["DefaultConnection"])
				);

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

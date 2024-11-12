using Microsoft.EntityFrameworkCore;
using Pokemon.Business.Contracts;
using Pokemon.Business.Impl;
using Pokemon.Infrastructure.Contracts;
using Pokemon.Infrastructure.Impl;
using Pokemon.Infrastructure.Impl.Context;

namespace Pokemon.WebApi
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

			builder.Services.AddScoped<IPokeApiImporter, PokeApiImporter>();
			builder.Services.AddScoped<IPokemonService, PokemonService>();
			builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();

			builder.Services.AddDbContext<PokemonApiContext>(options => options.UseSqlServer(builder.Configuration["DefaultConnection"]));

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

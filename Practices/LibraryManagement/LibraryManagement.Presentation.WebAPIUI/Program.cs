using LibraryManagement.Application.Contracts;
using LibraryManagement.Application.Impl;
using LibraryManagement.Infrastructure.Contracts;
using LibraryManagement.Infrastructure.Contracts.Context;
using LibraryManagement.InfrastructureImpl;
using Microsoft.EntityFrameworkCore;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		//add scoped of interfaces
		builder.Services.AddScoped<ILibraryService, LibraryService>();
		builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();
		builder.Services.AddScoped<IBooksRepository, BooksRepository>();
		builder.Services.AddScoped<IPurchasesRepository, PurchasesRepository>();
		builder.Services.AddDbContext<LibraryManagementContext>(options => options.UseSqlServer(
				builder.Configuration.GetConnectionString("DefaultConnection"),
				sqlOptions => sqlOptions.EnableRetryOnFailure(
					maxRetryCount: 5,
					maxRetryDelay: TimeSpan.FromSeconds(10),
					errorNumbersToAdd: null)
				));

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
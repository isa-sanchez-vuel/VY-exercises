using FriendshipManagement.Application.Contracts;
using FriendshipManagement.Application.Impl;
using FriendshipManagement.Infrastructure.Contracts;
using FriendshipManagement.Infrastructure.Contracts.Context;
using FriendshipManagement.Infrastructure.Impl;
using Microsoft.EntityFrameworkCore;

namespace FriendshipManagement.Presentation.WebAPI
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

			builder.Services.AddScoped<IFriendshipService, FriendshipService>();
			builder.Services.AddScoped<IFriendshipRepository, FriendshipRepository>();
			builder.Services.AddScoped<IPonyRepository, PonyRepository>();

			builder.Services.AddDbContext<FriendshipManagementContext>(
				context => context.UseSqlServer(builder.Configuration["DefaultConnection"])
				);

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

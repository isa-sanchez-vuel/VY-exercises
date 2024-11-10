using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Application.Impl;
using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Contracts.Data;
using OOPBankMultiuser.Infrastructure.Impl;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using OOPBankMultiuser.Presentation.WebAPIUI.SwaggerConfig;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IMovementRepository, MovementRepository>();

builder.Services.AddDbContext<OOPBankMultiuserContext>(options =>options.UseSqlServer(
		builder.Configuration["DefaultConnection"]));

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigOptions>();

builder.Services.AddApiVersioning(options =>
{
	options.DefaultApiVersion = new ApiVersion(1, 0);
	options.AssumeDefaultVersionWhenUnspecified = true;
	options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
	options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

var versionDescProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		foreach(var desc in versionDescProvider.ApiVersionDescriptions)
		{
			options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", $"Bank Account - {desc.GroupName.ToUpper()}");

		}
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

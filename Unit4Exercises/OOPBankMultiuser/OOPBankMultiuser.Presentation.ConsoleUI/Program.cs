using Microsoft.Extensions.DependencyInjection;
using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Application.Impl;
using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Impl;
using OOPBankMultiuser.Presentation.ConsoleUI;

ServiceCollection services = new();
ServiceProvider serviceProvider = services
	.AddScoped<IBankService, BankService>()
	.AddScoped<IAccountService, AccountService>()
	.AddScoped<IBankRepository, BankRepository>()
	.AddScoped<IAccountRepository, AccountRepository>()
	.AddScoped<IMovementRepository, MovementRepository>()
	.AddScoped<MainMenu>()
	.BuildServiceProvider();

MainMenu? mm = serviceProvider.GetService<MainMenu>();

mm?.StartApplication();

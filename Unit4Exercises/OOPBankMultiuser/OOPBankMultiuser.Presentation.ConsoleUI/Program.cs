using Microsoft.Extensions.DependencyInjection;
using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Impl;

namespace OOPBankMultiuser.Presentation.ConsoleUI
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ServiceCollection services = new();
			ServiceProvider serviceProvider = services

				.AddScoped<MainMenu>()
				.BuildServiceProvider();
			
			MainMenu? mm = serviceProvider.GetService<MainMenu>();

			mm.StartApplication();
		}
	}
}

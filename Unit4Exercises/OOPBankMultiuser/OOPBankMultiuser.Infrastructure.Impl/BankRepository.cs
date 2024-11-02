using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;
using System;
namespace OOPBankMultiuser.Infrastructure.Impl
{
	public class BankRepository : IBankRepository
	{
		private static List<BankEntity> simulatedAccountDBTable = new()
		{
			new()
			{
				BankId = "3000",
				ControlNum = "54", 
				OfficeId = "4790",
				EntityName = "PoatatoBank S.L", 
				CountryCode = "ES",
			}
		};
		public BankEntity GetBankInfo()
		{
			return simulatedAccountDBTable.First();
		}


	}
}

using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;


namespace OOPBankMultiuser.Infrastructure.Impl
{
	public class AccountRepository : IAccountRepository
	{
		private static List<AccountEntity> simulatedAccountDBTable = new()
		{
			new()
			{
				OwnerName = "Pepito Grillo",
				Iban = "ES0000000000000000000001",
				AccountNumber = "0000000001",
				Pin = "0001",
				TotalBalance = 0.0m
			}
		};

		public AccountEntity? GetAccountInfo()
		{
			return simulatedAccountDBTable.First();
		}

		public void UpdateAccount(AccountEntity updatedEntity)
		{
			AccountEntity currentEntity = simulatedAccountDBTable.First();

			currentEntity.TotalBalance = updatedEntity.TotalBalance;
			currentEntity.AccountNumber = updatedEntity.AccountNumber;
			currentEntity.Pin = updatedEntity.Pin;
		}
	}
}

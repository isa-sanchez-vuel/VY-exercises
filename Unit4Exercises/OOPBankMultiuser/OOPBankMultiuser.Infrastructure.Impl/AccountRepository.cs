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
				Iban = "ES54 3000 4790 54 0000000000",
				AccountNumber = "0000000000",
				Pin = "0000",
				TotalBalance = 500.0m
			},
			new()
			{
				OwnerName = "Francisco Bezerra",
				Iban = "ES54 3000 4790 54 0000000001",
				AccountNumber = "0000000001",
				Pin = "1111",
				TotalBalance = 50.0m
			},
			new()
			{
				OwnerName = "Laura Bastión",
				Iban = "ES54 3000 4790 54 0000000002",
				AccountNumber = "0000000002",
				Pin = "2222",
				TotalBalance = 1000.0m
			},
		}; 
		
		private static string CurrentLoggedAccount = "0000000000";

		public void SetCurrentAccount(string accountNumber)
		{
			CurrentLoggedAccount = accountNumber;
		}

		public string GetCurrentLoggedId()
		{
			return CurrentLoggedAccount;
		}

		public AccountEntity? GetAccountInfo()
		{
			return simulatedAccountDBTable
				.FirstOrDefault(accountEntity => accountEntity.AccountNumber == CurrentLoggedAccount);
		}

		public void AddAccount(AccountEntity newEntity)
		{
			simulatedAccountDBTable.Add(newEntity);
		}

		public void UpdateAccount(AccountEntity updatedEntity)
		{
			AccountEntity? currentEntity = simulatedAccountDBTable
				.FirstOrDefault(acc => acc.AccountNumber == updatedEntity.AccountNumber);

			if (currentEntity != null)
			{
				currentEntity.OwnerName = updatedEntity.OwnerName;
				currentEntity.TotalBalance = updatedEntity.TotalBalance;
				currentEntity.Pin = updatedEntity.Pin;
			}
		}

	}
}

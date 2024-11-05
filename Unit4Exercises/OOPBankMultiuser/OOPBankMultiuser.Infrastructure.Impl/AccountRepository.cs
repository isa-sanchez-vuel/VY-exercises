using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Contracts.Data;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;


namespace OOPBankMultiuser.Infrastructure.Impl
{
	public class AccountRepository : IAccountRepository
	{
		/*private static List<Account> simulatedAccountDBTable = new()
		{
			new()
			{
				Name = "Pepito Grillo",
				Iban = "ES54 3000 4790 54 0000000000",
				IdNumber = 1,
				Pin = "1111",
				Balance = 500.0m
			},
			new()
			{
				Name = "Francisco Bezerra",
				Iban = "ES54 3000 4790 54 0000000001",
				IdNumber = 2,
				Pin = "2222",
				Balance = 50.0m
			},
			new()
			{
				Name = "Laura Bastión",
				Iban = "ES54 3000 4790 54 0000000002",
				IdNumber = 3,
				Pin = "3333",
				Balance = 1000.0m
			},
		};*/

		private readonly OOPBankMultiuserContext _context = new();

		public AccountRepository(OOPBankMultiuserContext DBContext)
		{
			_context = DBContext;
		}

		private static int CurrentLoggedAccount = 1; 

		public void SetCurrentAccount(int accountNumber)
		{
			CurrentLoggedAccount = accountNumber;
		}

		public int GetCurrentLoggedId()
		{
			return CurrentLoggedAccount;
		}

		public bool AccountExists(int id)
		{
			return (_context.Accounts?.Any(e => e.IdNumber == id)).GetValueOrDefault();
		}

		public Account? GetAccountInfo(int accountNumber)
		{
			return _context.Accounts
				.FirstOrDefault(accountEntity => accountEntity.IdNumber == accountNumber);
		}

		public void AddAccount(Account newEntity)
		{
			_context.Add(newEntity);
		}

		public void UpdateAccount(Account updatedEntity)
		{
			Account? currentEntity = _context.Accounts
				.FirstOrDefault(acc => acc.IdNumber == updatedEntity.IdNumber);

			if (currentEntity != null)
			{
				currentEntity.Name = updatedEntity.Name;
				currentEntity.Balance = updatedEntity.Balance;
				currentEntity.Pin = updatedEntity.Pin;
			}
		}

	}
}

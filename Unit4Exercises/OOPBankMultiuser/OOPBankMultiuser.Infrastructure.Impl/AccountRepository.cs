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
				.FirstOrDefault(acc => acc.IdNumber == accountNumber);
		}

		public Account? AddAccount(Account newEntity)
		{
			_context.Add(newEntity);
			_context.SaveChanges();

			//TODO detecta el id como null, es muy probable que no se esté
			//creando la cuenta correctamente en la bbdd y no esté creando el id

			return newEntity;
		}

		public void UpdateAccount(Account updatedEntity)
		{
			Account? entity = _context.Accounts
				.Find(updatedEntity.IdNumber);

			if (entity != null)
			{
				entity.Name = updatedEntity.Name;
				entity.Balance = updatedEntity.Balance;
				entity.Pin = updatedEntity.Pin;
				entity.AccountNumber = updatedEntity.AccountNumber;
				entity.Iban = updatedEntity.Iban;
			}

			_context.SaveChanges();
		}

	}
}

using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Contracts.Data;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;


namespace OOPBankMultiuser.Infrastructure.Impl
{
	public class AccountRepository : IAccountRepository
	{
		private readonly OOPBankMultiuserContext _context = new();

		public AccountRepository(OOPBankMultiuserContext DBContext)
		{
			_context = DBContext;
		}

		private static int CurrentLoggedAccount = 0; 

		public void SetCurrentAccount(int accountNumber)
		{
			CurrentLoggedAccount = accountNumber;
		}
		
		public int GetCurrentAccount()
		{
			return CurrentLoggedAccount;
		}

		public Account? AccountExists(string accountNumber)
		{
			return _context.Accounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber);
		}

		public Account? GetAccountInfo()
		{
			return _context.Accounts
				.FirstOrDefault(acc => acc.IdNumber == CurrentLoggedAccount);
		}

		public Account? AddAccount(Account newEntity)
		{
			_context.Add(newEntity);
			_context.SaveChanges();

			return newEntity;
		}

		public bool UpdateAccount(Account updatedEntity)
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
				_context.SaveChanges();

				return true; //operation success
			}
			else return false; //operation failure
		}

	}
}

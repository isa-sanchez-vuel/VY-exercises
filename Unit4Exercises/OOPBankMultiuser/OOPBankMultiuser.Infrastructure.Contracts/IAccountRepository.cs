using OOPBankMultiuser.Infrastructure.Contracts.Entities;

namespace OOPBankMultiuser.Infrastructure.Contracts
{
	public interface IAccountRepository
	{
		void SetCurrentAccount(int accountNumber);
		int GetCurrentLoggedId();
		bool AccountExists(int id);
		Account? GetAccountInfo(int accountNumber);
		public void AddAccount(Account newEntity);
		void UpdateAccount(Account updatedEntity);

	}
}

using OOPBankMultiuser.Infrastructure.Contracts.Entities;

namespace OOPBankMultiuser.Infrastructure.Contracts
{
	public interface IAccountRepository
	{
		
		void SetCurrentAccount(int accountNumber);
		int GetCurrentAccount();
		Account? AccountExists(string accountNumber);
		Account? GetAccountInfo();
		Account? AddAccount(Account newEntity);
		bool UpdateAccount(Account updatedEntity);

	}
}

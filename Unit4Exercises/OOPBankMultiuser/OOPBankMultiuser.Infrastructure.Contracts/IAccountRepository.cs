using OOPBankMultiuser.Infrastructure.Contracts.Entities;

namespace OOPBankMultiuser.Infrastructure.Contracts
{
	public interface IAccountRepository
	{
		bool AccountExists(int id);
		Account? GetAccountInfo(int accountNumber);
		List<Account>? GetAllAccounts();
		Account? AddAccount(Account newEntity);
		bool UpdateAccount(Account updatedEntity);
		bool DeleteAccount(Account entity);
	}
}

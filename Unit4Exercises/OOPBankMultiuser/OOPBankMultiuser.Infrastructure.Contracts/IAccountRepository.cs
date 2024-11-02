using OOPBankMultiuser.Infrastructure.Contracts.Entities;

namespace OOPBankMultiuser.Infrastructure.Contracts
{
	public interface IAccountRepository
	{
		void SetCurrentAccount(string accountNumber);
		string GetCurrentLoggedId();
		AccountEntity? GetAccountInfo();
		public void AddAccount(AccountEntity newEntity);
		void UpdateAccount(AccountEntity updatedEntity);

	}
}

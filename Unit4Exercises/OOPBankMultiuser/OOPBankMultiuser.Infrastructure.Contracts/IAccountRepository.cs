using OOPBankMultiuser.Infrastructure.Contracts.Entities;

namespace OOPBankMultiuser.Infrastructure.Contracts
{
	public interface IAccountRepository
	{
		AccountEntity? GetAccountInfo();
		void UpdateAccount(AccountEntity updatedEntity);
	}
}

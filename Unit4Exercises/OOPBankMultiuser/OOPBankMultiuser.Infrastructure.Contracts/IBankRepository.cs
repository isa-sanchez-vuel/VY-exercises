using OOPBankMultiuser.Infrastructure.Contracts.Entities;

namespace OOPBankMultiuser.Infrastructure.Contracts
{
	public interface IBankRepository
	{
		BankEntity GetBankInfo();
	}
}

using OOPBankMultiuser.Application.Contracts.DTOs;
using OOPBankMultiuser.Business.Contracts.DTOs;

namespace OOPBankMultiuser.Business.Contracts
{
	public interface IAccountService
	{
		IncomeResultDTO DepositMoney(decimal income);
		OutcomeResultDTO WithdrawMoney(decimal income);
		MovementListDTO GetAllMovements();
		MovementListDTO GetIncomes();
		MovementListDTO GetOutcomes();
		decimal? GetBalance();
		AccountResultDTO? GetAccountInfo();
	}
}

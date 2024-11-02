using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;

namespace OOPBankMultiuser.Application.Contracts
{
    public interface IAccountService
	{
		IncomeResultDTO DepositMoney(decimal income);
		OutcomeResultDTO WithdrawMoney(decimal income);
		MovementListDTO GetAllMovements();
		MovementListDTO GetIncomes();
		MovementListDTO GetOutcomes();
		decimal? GetBalance();
		AccountDTO? GetAccountInfo();
		AccountDTO? GetAccountInfo(string accountNumber);
	}
}

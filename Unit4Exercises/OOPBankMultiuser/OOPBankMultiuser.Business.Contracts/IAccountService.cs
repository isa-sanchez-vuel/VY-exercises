using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;

namespace OOPBankMultiuser.Application.Contracts
{
    public interface IAccountService
	{
		IncomeResultDTO DepositMoney(decimal income, int accountNumber);
		OutcomeResultDTO WithdrawMoney(decimal income, int accountNumber);
		MovementListDTO GetAllMovements(int accountNumber);
		MovementListDTO GetIncomes(int accountNumber);
		MovementListDTO GetOutcomes(int accountNumber);
		decimal? GetBalance(int accountNumber);
		AccountDTO? GetAccountInfo(int accountNumber);
	}
}

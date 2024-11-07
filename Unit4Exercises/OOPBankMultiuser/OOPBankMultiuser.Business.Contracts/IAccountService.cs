using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.BankOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.DatabaseOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;

namespace OOPBankMultiuser.Application.Contracts
{
    public interface IAccountService
	{
		IncomeResultDTO DepositMoney(decimal income);
		OutcomeResultDTO WithdrawMoney(decimal outcome);
		MovementListDTO GetAllMovements();
		MovementListDTO GetIncomes();
		MovementListDTO GetOutcomes();
		BalanceDTO? GetBalance();
		AccountDTO? GetAccountInfo();
		CreateAccountResultDTO CreateAccount(CreateAccountDTO newAccount);
		UpdateAccountResultDTO UpdateAccount(UpdateAccountDTO modifiedAccount);
		LoginResultDTO LoginAccount(LoginDTO loginRequest);
	}
}

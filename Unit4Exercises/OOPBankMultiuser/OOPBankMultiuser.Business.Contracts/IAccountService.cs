using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.DatabaseOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;

namespace OOPBankMultiuser.Application.Contracts
{
    public interface IAccountService
	{
		IncomeResultDTO DepositMoney(decimal income, int idNumber);
		OutcomeResultDTO WithdrawMoney(decimal income, int idNumber);
		MovementListDTO GetAllMovements(int idNumber);
		MovementListDTO GetIncomes(int idNumber);
		MovementListDTO GetOutcomes(int idNumber);
		BalanceDTO? GetBalance(int idNumber);
		AccountDTO? GetAccountInfo(int idNumber);
		AccountListDTO GetAllAccounts();
		CreateAccountResultDTO CreateAccount(CreateAccountDTO newAccount);
		UpdateAccountResultDTO UpdateAccount(UpdateAccountDTO modifiedAccount);
		DeleteAccountResultDTO DeleteAccount(int idNumber);
	}
}

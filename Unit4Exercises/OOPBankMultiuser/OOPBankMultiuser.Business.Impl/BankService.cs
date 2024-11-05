using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Application.Contracts.DTOs.BankOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;
using OOPBankMultiuser.Domain.Models;
using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;
using OOPBankMultiuser.XCutting.Enums;

namespace OOPBankMultiuser.Application.Impl
{
    public class BankService : IBankService
	{

		private readonly IAccountRepository? _accountRepository;
		private readonly IBankRepository? _bankRepository;
		private readonly IAccountService? _accountService;

		public BankService(IAccountRepository accountRepository, IBankRepository bankRepository, IAccountService accountService)
		{
			_accountRepository = accountRepository;
			_bankRepository = bankRepository;
			_accountService = accountService;
		}

		//public CreateAccountResultDTO CreateAccount(string ownerName, int accountNumber, string pin)
		//{
		//	CreateAccountResultDTO result = new()
		//	{
		//		HasErrors = false,
		//		Error = null,
		//	};

		//	AccountModel? accountModel = new();

		//	if (accountModel.ValidateCredentials(AccountModel.GenerateAccountNumber(accountNumber), pin))
		//	{
		//		if (_bankRepository != null && _accountRepository != null)
		//		{
		//			BankEntity bankEntity = _bankRepository.GetBankInfo();

		//			if (bankEntity != null)
		//			{
		//				BankModel bankModel = new()
		//				{
		//					CountryCode = bankEntity.CountryCode,
		//					BankId = bankEntity.BankId,
		//					OfficeId = bankEntity.OfficeId,
		//					ControlNum = bankEntity.ControlNum,
		//				};

		//				bankModel.CreateAccount(accountNumber, pin, ownerName);

		//				accountModel = bankModel.FindAccount(accountNumber, pin);

		//				if (accountModel != null)
		//				{
		//					Account newAccountEntity = new()
		//					{
		//						Name = accountModel.OwnerName,
		//						Pin = accountModel.Pin,
		//					};
		//					_accountRepository?.AddAccount(newAccountEntity);
		//				}
		//				else
		//				{
		//					result.HasErrors = true;
		//					result.Error = CreateAccountErrorEnum.ErrorCreatingAccount;
		//				}
		//			}
		//		}
		//	}
		//	else
		//	{
		//		result.HasErrors = true;
		//		result.AccountNumberLength = AccountModel.ACCOUNT_LENGTH;
		//		result.PinLength = AccountModel.PIN_LENGTH;

		//		if (accountModel.numberSizeWrong) result.Error = CreateAccountErrorEnum.NumberLength;
		//		else if (accountModel.numberFormatWrong) result.Error = CreateAccountErrorEnum.NumberFormat;
		//		else if (accountModel.pinSizeWrong) result.Error = CreateAccountErrorEnum.PinLenght;
		//		else if (accountModel.pinFormatWrong) result.Error = CreateAccountErrorEnum.PinFormat;
		//	}

		//	return result;
		//}

		public LoginResultDTO LoginAccount(string accountNumber, string pin)
		{
			LoginResultDTO? result = new()
			{
				HasErrors = false,
				Error = null,
			};

			AccountModel accountModel = new();

			if (accountModel.ValidateCredentials(accountNumber, pin))
			{
				if (_accountRepository != null)
				{
					_accountRepository.SetCurrentAccount(int.Parse(accountNumber));
					Account? accountEntity = _accountRepository?.GetAccountInfo(int.Parse(accountNumber));

					if (accountEntity != null)
					{
						AccountDTO? accountDto = _accountService.GetAccountInfo(int.Parse(accountNumber));

						if (accountDto != null) result.Account = accountDto;
					}
					else
					{
						result.HasErrors = true;
						result.Error = LoginErrorEnum.AccountNotFound;
					}
				}
			}
			else
			{
				result.HasErrors = true;
				result.AccountNumberLength = AccountModel.ACCOUNT_LENGTH;
				result.PinLength = AccountModel.PIN_LENGTH;

				if (accountModel.numberSizeWrong) result.Error = LoginErrorEnum.NumberLength;
				else if (accountModel.numberFormatWrong) result.Error = LoginErrorEnum.NumberFormat;
				else if (accountModel.pinSizeWrong) result.Error = LoginErrorEnum.PinLenght;
				else if (accountModel.pinFormatWrong) result.Error = LoginErrorEnum.PinFormat;
			}
			return result;
		}

		public void SetCurrentAccount(AccountDTO account)
		{
			_accountRepository?.SetCurrentAccount(account.IdNumber);
		}


	}
}

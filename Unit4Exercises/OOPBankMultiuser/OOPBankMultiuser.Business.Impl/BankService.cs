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

		public BankService(IAccountRepository accountRepository, IBankRepository bankRepository)
		{
			_accountRepository = accountRepository;
			_bankRepository = bankRepository;
		}

		public CreateAccountResultDTO CreateAccount(string ownerName, string accountNumber, string pin)
		{
			CreateAccountResultDTO result = new()
			{
				HasErrors = false,
				Error = null,
			};

			AccountModel? accountModel = new();

			if (accountModel.ValidateCredentials(accountNumber, pin))
			{
				if (_bankRepository != null && _accountRepository != null)
				{
					BankEntity bankEntity = _bankRepository.GetBankInfo();

					if (bankEntity != null)
					{
						BankModel bankModel = new()
						{
							CountryCode = bankEntity.CountryCode,
							BankId = bankEntity.BankId,
							OfficeId = bankEntity.OfficeId,
							ControlNum = bankEntity.ControlNum,
						};

						bankModel.CreateAccount(accountNumber, pin, ownerName);

						accountModel = bankModel.FindAccount(accountNumber, pin);

						if (accountModel != null)
						{
							AccountEntity newAccountEntity = new()
							{
								OwnerName = accountModel.OwnerName,
								AccountNumber = accountModel.AccountNumber,
								Pin = accountModel.Pin,
							};
							_accountRepository?.AddAccount(newAccountEntity);
						}
						else
						{
							result.HasErrors = true;
							result.Error = CreateAccountErrorEnum.ErrorCreatingAccount;
						}
					}
				}
			}
			else
			{
				result.HasErrors = true;
				result.AccountNumberLength = AccountModel.ACCOUNT_LENGTH;
				result.PinLength = AccountModel.PIN_LENGTH;

				if (accountModel.numberSizeWrong) result.Error = CreateAccountErrorEnum.NumberLength;
				else if (accountModel.numberFormatWrong) result.Error = CreateAccountErrorEnum.NumberFormat;
				else if (accountModel.pinSizeWrong) result.Error = CreateAccountErrorEnum.PinLenght;
				else if (accountModel.pinFormatWrong) result.Error = CreateAccountErrorEnum.PinFormat;
			}

			return result;
		}

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
					_accountRepository.SetCurrentAccount(accountNumber);
					AccountEntity? accountEntity = _accountRepository?.GetAccountInfo();

					if (accountEntity != null)
					{
						AccountDTO? accountDto = GetCurrentAccount();

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

		public AccountDTO? GetCurrentAccount()
		{
			AccountDTO? result = null;
			if (_accountRepository != null)
			{
				AccountEntity? accountEntity = _accountRepository?.GetAccountInfo();

				if (accountEntity != null)
				{
					result = new()
					{
						AccountNumber = accountEntity.AccountNumber,
						Pin = accountEntity.Pin,
						OwnerName = accountEntity.OwnerName,
						TotalBalance = accountEntity.TotalBalance,
						Iban = accountEntity.Iban,
					};
				}
			}
			return result;
		}

		public void SetCurrentAccount(AccountDTO account)
		{
			_accountRepository?.SetCurrentAccount(account.AccountNumber);
		}


	}
}

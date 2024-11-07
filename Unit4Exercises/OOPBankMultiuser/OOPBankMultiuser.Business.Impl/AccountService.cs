using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Domain.Models;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;
using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.XCutting.Enums;
using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;
using OOPBankMultiuser.Application.Contracts.DTOs.DatabaseOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.BankOperations;

namespace OOPBankMultiuser.Application.Impl
{
    public class AccountService : IAccountService
	{
		private readonly IAccountRepository? _accountRepository;
		private readonly IMovementRepository? _movementRepository;

		public AccountService(IAccountRepository accountRepository, IMovementRepository movementRepository)
		{
			_accountRepository = accountRepository;
			_movementRepository = movementRepository;
		}

		public IncomeResultDTO DepositMoney(decimal income)
		{
			//initialize result and model
			IncomeResultDTO result = new()
			{
				ResultHasErrors = false,
				Error = null
			};

			AccountModel accountModel = new();
			//int currentLoggedId = _accountRepository.GetCurrentLoggedId();

			if (accountModel.ValidateIncome(income))
			{
				//get accountDto and movement entities
				
				Account? accountEntity = _accountRepository?.GetAccountInfo();
				List<Movement>? movementEntityList = _movementRepository?.GetMovements(_accountRepository.GetCurrentAccount());

				if (accountEntity != null && movementEntityList != null)
				{
					//map entity to model (infrastructure to domain)
					accountModel.TotalBalance = accountEntity.Balance;
					accountModel.Movements = movementEntityList.Select(movementEntity => new MovementModel
					{
						Date = movementEntity.Timestamp,
						Content = movementEntity.Value,
					}).ToList();

					//apply service logic to model (business in domain)
					accountModel.AddIncome(income);

					Movement newMovement = new()
					{
						Timestamp = accountModel.Movements.Last().Date,
						Value = accountModel.Movements.Last().Content,
					};

					//map (send) model result to entity and dto
					accountEntity.Balance = accountModel.TotalBalance;

					result.TotalBalance = accountModel.TotalBalance;

					//update entity
					_accountRepository?.UpdateAccount(accountEntity);
					_movementRepository?.AddMovement(newMovement);
				}
			}
			
			else
			{
				result.ResultHasErrors = true;
				result.MaxIncomeAllowed = AccountModel.MAX_INCOME;

				if (accountModel.incomeNegative) result.Error = IncomeErrorEnum.NegativeValue;
				if (accountModel.incomeOverMaxValue) result.Error = IncomeErrorEnum.OverMaxIncome;
			}

			return result;
		}

		public OutcomeResultDTO WithdrawMoney(decimal outcome)
		{
			OutcomeResultDTO result = new()
			{
				ResultHasErrors = false,
				Error = null
			};

			AccountModel accountModel = new();
			
			Account? accountEntity = _accountRepository?.GetAccountInfo();
			List<Movement>? movementEntityList = _movementRepository?.GetMovements(_accountRepository.GetCurrentAccount());

			if (accountEntity != null && movementEntityList != null)
			{
				accountModel.TotalBalance = accountEntity.Balance;

				accountModel.Movements = movementEntityList.Select(movementEntity => new MovementModel
				{
					Date = movementEntity.Timestamp,
					Content = movementEntity.Value,
				}).ToList();


				if (accountModel.ValidateOutcome(outcome))
				{
					accountModel.SubtractOutcome(outcome);

					Movement newMovement = new()
					{
						Timestamp = accountModel.Movements.Last().Date,
						Value = accountModel.Movements.Last().Content,
					};

					accountEntity.Balance = accountModel.TotalBalance;

					result.TotalBalance = accountModel.TotalBalance;

					_accountRepository?.UpdateAccount(accountEntity);
					_movementRepository?.AddMovement(newMovement);
				}
				else
				{
					result.ResultHasErrors = true;
					result.MaxOutcomeAllowed = AccountModel.MAX_OUTCOME;
					result.TotalBalance = accountModel.TotalBalance;

					if (accountModel.outcomeOverMaxValue) result.Error = OutcomeErrorEnum.OverMaxOutcome;
					if (accountModel.outcomeNegative) result.Error = OutcomeErrorEnum.NegativeValue;
					if (accountModel.outcomeOverTotalBalance) result.Error = OutcomeErrorEnum.OverTotalBalance;
				}
				
			}

			return result;
		}

		public MovementListDTO GetAllMovements()
		{
			MovementListDTO result = new()
			{
				HasErrors = false,
				Error = null,
			};

			List<Movement> movementEntityList = _movementRepository?.GetMovements(_accountRepository.GetCurrentAccount())!;

			if (movementEntityList != null)
			{
				return new()
				{
					Movements = movementEntityList.Select(movementEntity => new MovementDTO
					{
						Timestamp = movementEntity.Timestamp,
						Content = movementEntity.Value,

					}).ToList(),
					TotalBalance = movementEntityList.Sum(movement => movement.Value)
				};
			}
			else
			{
				result.HasErrors = true;
				result.Error = GetMovementsErrorEnum.MovementsNotFound;
			}
			return result;
		}

		public MovementListDTO GetIncomes()
		{
			MovementListDTO result = new()
			{
				HasErrors = false,
				Error = null,
			};

			List<Movement> incomeEntitiesList = _movementRepository?.GetMovements(_accountRepository.GetCurrentAccount()).Where(movement => movement.Value > 0).ToList()!;

			if (incomeEntitiesList != null)
			{

				result = new()
				{
					Movements = incomeEntitiesList.Select(movementEntity => new MovementDTO
					{
						Timestamp = movementEntity.Timestamp,
						Content = movementEntity.Value,

					}).ToList(),
					TotalIncome = incomeEntitiesList.Sum(movement => movement.Value)
				};
			}
			else
			{
				result.HasErrors = true;
				result.Error = GetMovementsErrorEnum.MovementsNotFound;
			}
			return result;
		}

		public MovementListDTO GetOutcomes()
		{
			MovementListDTO result = new()
			{
				HasErrors = false,
				Error = null,
			};

			//int currentLoggedId = _accountRepository.GetCurrentLoggedId();
			
				List<Movement> outcomeEntitiesList = _movementRepository?.GetMovements(_accountRepository.GetCurrentAccount()).Where(movement => movement.Value < 0).ToList()!;

				if (outcomeEntitiesList != null)
				{
					result = new()
					{
						Movements = outcomeEntitiesList.Select(movementEntity => new MovementDTO
						{
							Timestamp = movementEntity.Timestamp,
							Content = movementEntity.Value,

						}).ToList(),
						TotalOutcome = outcomeEntitiesList.Sum(movement => movement.Value)
					};
				}
				else
				{
					result.HasErrors = true;
					result.Error = GetMovementsErrorEnum.MovementsNotFound;
				}
			
			return result;
		}

		public BalanceDTO? GetBalance()
		{
			Account? accountEntity = _accountRepository?.GetAccountInfo();

			if (accountEntity == null) throw new Exception();

			return new()
			{
				TotalBalance = accountEntity.Balance,
			};
		}

		public AccountDTO? GetAccountInfo()
		{
			Account? accountEntity = _accountRepository?.GetAccountInfo();

			if (accountEntity == null) throw new Exception();

			AccountDTO result = new()
			{
				OwnerName = accountEntity.Name,
				IdNumber = accountEntity.IdNumber,
				Pin = accountEntity.Pin,
				TotalBalance = accountEntity.Balance,
				Iban = accountEntity.Iban,
			};

			return result;
		}


		public CreateAccountResultDTO CreateAccount(CreateAccountDTO newAccount)
		{
			CreateAccountResultDTO result = new()
			{
				HasErrors = false,
				Error = null,
			};

			AccountModel model = new();
			if (model.ValidatePin(newAccount.Pin))
			{

				Account? entity = new()
				{
					Name = newAccount.Name,
					Pin = newAccount.Pin,
					Balance = newAccount.InitialBalance,
				};

				entity = _accountRepository?.AddAccount(entity);

				if (entity != null)
				{

					string accountNumber = AccountModel.GenerateAccountNumber(entity.IdNumber);
					string iban = AccountModel.CreateIban(accountNumber);

					AccountDTO accountDto = new()
					{
						OwnerName = entity.Name,
						IdNumber = entity.IdNumber,
						AccountNumber = accountNumber,
						Pin = entity.Pin,
						Iban = iban,
					};

					entity.Iban = accountDto.Iban;
					entity.AccountNumber = accountDto.AccountNumber;

					_accountRepository?.UpdateAccount(entity);

					result.Account = accountDto;

				}
				else
				{
					result.HasErrors = true;
					result.Error = CreateAccountErrorEnum.ErrorCreatingAccount;
				}
			}
			else
			{
				result.HasErrors = true;
				result.PinLength = AccountModel.PIN_LENGTH;
				if (model.pinFormatWrong) result.Error = CreateAccountErrorEnum.PinLenght;
				else if (model.pinSizeWrong) result.Error = CreateAccountErrorEnum.PinFormat;
			}

			return result;
		}
		
		public UpdateAccountResultDTO UpdateAccount(UpdateAccountDTO modifiedAccount)
		{
			UpdateAccountResultDTO result = new()
			{
				HasErrors = false,
				Error = null,
			};

			AccountModel model = new();
			if (model.ValidatePin(modifiedAccount.NewPin))
			{
				Account? entity = _accountRepository?.GetAccountInfo();

				if (entity != null)
				{
					AccountDTO oldAccount = new()
					{
						OwnerName = entity.Name,
						IdNumber = entity.IdNumber,
						Pin = entity.Pin,
						AccountNumber = entity.AccountNumber,
						Iban = entity.Iban,
					};

					entity.Name = modifiedAccount.NewName;
					entity.Pin = modifiedAccount.NewPin;

					bool isSuccess = _accountRepository.UpdateAccount(entity);

					if (isSuccess)
					{
						result.OldAccount = oldAccount;
						result.Account = new()
						{
							OwnerName = entity.Name,
							IdNumber = entity.IdNumber,
							Pin = entity.Pin,
							AccountNumber = entity.AccountNumber,
							Iban = entity.Iban,
						};
					}
					else
					{
						result.HasErrors = true;
						result.Error = UpdateAccountErrorEnum.UpdateFailure;
					}

				}
				else
				{
					result.HasErrors = true;
					result.Error = UpdateAccountErrorEnum.AccountNotFound;
				}
			}
			else
			{
				result.HasErrors = true;
				if(model.pinSizeWrong) result.Error = UpdateAccountErrorEnum.PinLenght;
				else if(model.pinFormatWrong) result.Error = UpdateAccountErrorEnum.PinFormat;
			}


			return result;
		}

		public LoginResultDTO LoginAccount(LoginDTO loginRequest)
		{
			LoginResultDTO? result = new()
			{
				HasErrors = true,
				Error = null,
			};

			AccountModel accountModel = new();

			if (accountModel.ValidateCredentials(loginRequest.AccountNumber, loginRequest.Pin))
			{
				if (_accountRepository != null)
				{
					Account? accountEntity = _accountRepository?.AccountExists(loginRequest.AccountNumber);

					if (accountEntity != null)
					{
						if (loginRequest.Pin.Equals(accountEntity.Pin)) {
							
							result.Account = new()
							{
								IdNumber = accountEntity.IdNumber,
								AccountNumber = accountEntity.AccountNumber,
								Pin = accountEntity.Pin,
								Iban = accountEntity.Iban,
								TotalBalance = accountEntity.Balance,
							};

							_accountRepository.SetCurrentAccount(accountEntity.IdNumber);
							result.HasErrors = false;
						}
						else result.Error = LoginErrorEnum.WrongPin;
					}
					else result.Error = LoginErrorEnum.AccountNotFound;
				}
				else result.Error = LoginErrorEnum.ConnectionFailure;
			}
			else
			{
				result.AccountNumberLength = AccountModel.ACCOUNT_LENGTH;
				result.PinLength = AccountModel.PIN_LENGTH;

				if (accountModel.numberSizeWrong) result.Error = LoginErrorEnum.NumberLength;
				else if (accountModel.numberFormatWrong) result.Error = LoginErrorEnum.NumberFormat;
				else if (accountModel.pinSizeWrong) result.Error = LoginErrorEnum.PinLenght;
				else if (accountModel.pinFormatWrong) result.Error = LoginErrorEnum.PinFormat;
			}
			return result;
		}
	}
}

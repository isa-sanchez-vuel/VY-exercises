using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Domain.Models;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;
using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.XCutting.Enums;
using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;

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

			if (accountModel.ValidateIncome(income))
			{
				//get account and movement entities
				AccountEntity? accountEntity = _accountRepository?.GetAccountInfo();
				string? currentLoggedId = _accountRepository?.GetCurrentLoggedId();
				if (currentLoggedId != null)
				{
					List<MovementEntity>? movementEntityList = _movementRepository?.GetMovements(currentLoggedId);

					if (accountEntity != null && movementEntityList != null)
					{
						//map entity to model (infrastructure to domain)
						accountModel.TotalBalance = accountEntity.TotalBalance;
						accountModel.Movements = movementEntityList.Select(movementEntity => new MovementModel
						{
							Date = movementEntity.timestamp,
							Content = movementEntity.content,
						}).ToList();

						//apply service logic to model (business in domain)
						accountModel.AddIncome(income);

						MovementEntity newMovement = new()
						{
							timestamp = accountModel.Movements.Last().Date,
							content = accountModel.Movements.Last().Content,
						};

						//map (send) model result to entity and dto
						accountEntity.TotalBalance = accountModel.TotalBalance;

						result.TotalBalance = accountModel.TotalBalance;

						//update entity
						_accountRepository?.UpdateAccount(accountEntity);
						_movementRepository?.AddMovement(newMovement);
					}
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
			AccountEntity? accountEntity = _accountRepository?.GetAccountInfo();
			string? currentLoggedId = _accountRepository?.GetCurrentLoggedId();
			if (currentLoggedId != null)
			{
				List<MovementEntity>? movementEntityList = _movementRepository?.GetMovements(currentLoggedId);

				if (accountEntity != null && movementEntityList != null)
				{
					accountModel.TotalBalance = accountEntity.TotalBalance;

					accountModel.Movements = movementEntityList.Select(movementEntity => new MovementModel
					{
						Date = movementEntity.timestamp,
						Content = movementEntity.content,
					}).ToList();


					if (accountModel.ValidateOutcome(outcome))
					{
						accountModel.SubtractOutcome(outcome);

						MovementEntity newMovement = new()
						{
							timestamp = accountModel.Movements.Last().Date,
							content = accountModel.Movements.Last().Content,
						};

						accountEntity.TotalBalance = accountModel.TotalBalance;

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

			string? currentLoggedId = _accountRepository?.GetCurrentLoggedId();
			if (currentLoggedId != null)
			{
				List<MovementEntity> movementEntityList = _movementRepository?.GetMovements(currentLoggedId)!;

				if (movementEntityList != null)
				{
					return new()
					{
						Movements = movementEntityList.Select(movementEntity => new MovementDTO
						{
							Timestamp = movementEntity.timestamp,
							Content = movementEntity.content,
						}).ToList(),
						TotalBalance = movementEntityList.Sum(movement => movement.content)
					};
				}

				else
				{
					result.HasErrors = true;
					result.Error = GetMovementsErrorEnum.MovementsNotFound;
				}
			}
			else
			{
				result.HasErrors = true;
				result.Error = GetMovementsErrorEnum.AccountNotFound;
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

			string? currentLoggedId = _accountRepository?.GetCurrentLoggedId();
			if (currentLoggedId != null)
			{
				List<MovementEntity> incomeEntitiesList = _movementRepository?.GetMovements(currentLoggedId).Where(movement => movement.content > 0).ToList()!;

				if (incomeEntitiesList != null)
				{

					result = new()
					{
						Movements = incomeEntitiesList.Select(movementEntity => new MovementDTO
						{
							Timestamp = movementEntity.timestamp,
							Content = movementEntity.content,

						}).ToList(),
						TotalIncome = incomeEntitiesList.Sum(movement => movement.content)
					};
				}
				else
				{
					result.HasErrors = true;
					result.Error = GetMovementsErrorEnum.MovementsNotFound;
				}
			}
			else
			{
				result.HasErrors = true;
				result.Error = GetMovementsErrorEnum.AccountNotFound;
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

			string? currentLoggedId = _accountRepository?.GetCurrentLoggedId();
			if (currentLoggedId != null)
			{
				List<MovementEntity> outcomeEntitiesList = _movementRepository?.GetMovements(currentLoggedId).Where(movement => movement.content < 0).ToList()!;

				if (outcomeEntitiesList != null)
				{
					result = new()
					{
						Movements = outcomeEntitiesList.Select(movementEntity => new MovementDTO
						{
							Timestamp = movementEntity.timestamp,
							Content = movementEntity.content,

						}).ToList(),
						TotalIncome = outcomeEntitiesList.Sum(movement => movement.content)
					};
				}
				else
				{
					result.HasErrors = true;
					result.Error = GetMovementsErrorEnum.MovementsNotFound;
				}
			}
			else
			{
				result.HasErrors = true;
				result.Error = GetMovementsErrorEnum.AccountNotFound;
			}
			return result;
		}

		public decimal? GetBalance()
		{
			AccountEntity? accountEntity = _accountRepository?.GetAccountInfo();

			if (accountEntity == null) throw new Exception();

			AccountModel accountModel = new()
			{
				TotalBalance = accountEntity.TotalBalance,
			};

			return accountModel?.TotalBalance;
		}

		public AccountDTO? GetAccountInfo()
		{
			AccountEntity? accountEntity = _accountRepository?.GetAccountInfo();

			if (accountEntity == null) throw new Exception();

			AccountDTO result = new()
			{
				OwnerName = accountEntity.OwnerName,
				AccountNumber = accountEntity.AccountNumber,
				Pin = accountEntity.Pin,
				TotalBalance = accountEntity.TotalBalance,
				Iban = accountEntity.Iban,
			};

			return result;
		}

		public AccountDTO? GetAccountInfo(string accountNumber)
		{
			AccountEntity? accountEntity = _accountRepository?.GetAccountInfo();

			if (accountEntity == null) throw new Exception();

			AccountDTO result = new()
			{
				OwnerName = accountEntity.OwnerName,
				AccountNumber = accountEntity.AccountNumber,
				Pin = accountEntity.Pin,
				TotalBalance = accountEntity.TotalBalance,
				Iban = accountEntity.Iban,
			};



			return result;
		}
	}
}

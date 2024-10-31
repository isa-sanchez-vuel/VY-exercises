using OOPBankMultiuser.Business.Contracts;
using OOPBankMultiuser.Business.Contracts.DTOs;
using OOPBankMultiuser.Domain.Models;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;
using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Impl;
using OOPBankMultiuser.XCutting.Enums;
using OOPBankMultiuser.Application.Contracts.DTOs;

namespace OOPBankMultiuser.Business.Impl
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
				List<MovementEntity>? movementEntityList = _movementRepository?.GetMovements();

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

					//map (send) model result to entity
					accountEntity.TotalBalance = accountModel.TotalBalance;

					result.TotalBalance = accountModel.TotalBalance;

					//update entity
					_accountRepository?.UpdateAccount(accountEntity);
					_movementRepository?.AddMovement(newMovement);
				}
			}
			else
			{
				result.ResultHasErrors = true;

				if(accountModel.incomeNegative) result.Error = IncomeErrorEnum.NegativeValue;
				if (accountModel.incomeOverMaxValue)
				{
					result.Error = IncomeErrorEnum.OverMaxIncome;
					result.MaxIncomeAllowed = AccountModel.MAX_INCOME;
				}
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

			if (accountModel.ValidateOutcome(outcome))
			{
				AccountEntity? accountEntity = _accountRepository?.GetAccountInfo();
				List<MovementEntity>? movementsEntityList = _movementRepository?.GetMovements();

				if(accountEntity != null && movementsEntityList != null)
				{
					accountModel.TotalBalance = accountEntity.TotalBalance;

					accountModel.Movements = movementsEntityList.Select(movementEntity => new MovementModel
					{
						Date = movementEntity.timestamp,
						Content = movementEntity.content,
					}).ToList();

					accountModel.AddIncome(outcome);

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
			}
			else
			{
				result.ResultHasErrors = true;

				if (accountModel.outcomeOverMaxValue)
				{
					result.Error = OutcomeErrorEnum.OverMaxOutcome;
					result.MaxOutcomeAllowed = AccountModel.MAX_OUTCOME;
				}
				if (accountModel.outcomeNegative) result.Error = OutcomeErrorEnum.NegativeValue;
				if (accountModel.outcomeOverTotalBalance) result.Error = OutcomeErrorEnum.OverTotalBalance;
			}

			return result;
		}

		public MovementListDTO GetAllMovements()
		{
			List<MovementEntity>? movementEntitiesList = _movementRepository?.GetMovements();

			return new()
			{
				Movements = movementEntitiesList.Select(movementEntity => new MovementDTO
				{
					Timestamp = movementEntity.timestamp,
					Content = movementEntity.content,
				}).ToList(),
				TotalBalance = movementEntitiesList.Sum(movement => movement.content)
			};
		}

		public MovementListDTO GetIncomes()
		{
			List<MovementEntity> incomeEntitiesList = _movementRepository!.GetMovements().Where(movement => movement.content > 0).ToList();

			return new()
			{
				Movements = incomeEntitiesList.Select(movementEntity => new MovementDTO
				{
					Timestamp = movementEntity.timestamp,
					Content = movementEntity.content,

				}).ToList(),
				TotalIncome = incomeEntitiesList.Sum(movement => movement.content)
			};

		}

		public MovementListDTO GetOutcomes()
		{
			List<MovementEntity> outcomeEntitiesList = _movementRepository!.GetMovements().Where(movement => movement.content < 0).ToList();

			return new()
			{
				Movements = outcomeEntitiesList.Select(movementEntity => new MovementDTO
				{
					Timestamp = movementEntity.timestamp,
					Content = movementEntity.content,

				}).ToList(),
				TotalIncome = outcomeEntitiesList.Sum(movement => movement.content)
			};
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

		public AccountResultDTO? GetAccountInfo()
		{
			AccountEntity? accountEntity = _accountRepository?.GetAccountInfo();

			if (accountEntity == null) throw new Exception();

			AccountResultDTO result = new()
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

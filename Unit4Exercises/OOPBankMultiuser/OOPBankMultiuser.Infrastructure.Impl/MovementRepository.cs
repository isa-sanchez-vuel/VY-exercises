using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;

namespace OOPBankMultiuser.Infrastructure.Impl
{
	public class MovementRepository : IMovementRepository
	{
		private static List<MovementEntity> simulatedMovementsDBTable = new()
		{
			new()
			{
				accountId = "0000000000",
				timestamp = DateTime.Now.AddDays(-30),
				content = +700
			},
			new()
			{
				accountId = "0000000000",
				timestamp = DateTime.Now.AddDays(-10),
				content = -200
			},

			new()
			{
				accountId = "0000000001",
				timestamp = DateTime.Now.AddDays(-30),
				content = +200
			},
			new()
			{
				accountId = "0000000001",
				timestamp = DateTime.Now.AddDays(-10),
				content = -150
			},

			new()
			{
				accountId = "0000000002",
				timestamp = DateTime.Now.AddDays(-30),
				content = +3000
			},
			new()
			{
				accountId = "0000000002",
				timestamp = DateTime.Now.AddDays(-10),
				content = -2000
			},
		};

		public List<MovementEntity> GetMovements(string currentAccountNumber)
		{
			return simulatedMovementsDBTable.Where(x => x.accountId == currentAccountNumber).ToList();
		}

		public void AddMovement(MovementEntity newMovement)
		{
			simulatedMovementsDBTable.Add(newMovement);
		}
	}
}

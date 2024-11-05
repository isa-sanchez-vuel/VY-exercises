using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Contracts.Data;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;

namespace OOPBankMultiuser.Infrastructure.Impl
{
	public class MovementRepository : IMovementRepository
	{
		/*private static List<Movement> simulatedMovementsDBTable = new()
		{
			new()
			{
				Id = 1,
				AccountId = 1,
				Timestamp = DateTime.Now.AddDays(-30),
				Value = +700
			},
			new()
			{
				Id = 2,
				AccountId = 1,
				Timestamp = DateTime.Now.AddDays(-10),
				Value = -200
			},

			new()
			{
				Id = 3,
				AccountId = 2,
				Timestamp = DateTime.Now.AddDays(-30),
				Value = +200
			},
			new()
			{
				Id = 4,
				AccountId = 2,
				Timestamp = DateTime.Now.AddDays(-10),
				Value = -150
			},

			new()
			{
				Id = 5,
				AccountId = 3,
				Timestamp = DateTime.Now.AddDays(-30),
				Value = +3000
			},
			new()
			{
				Id = 6,
				AccountId = 3,
				Timestamp = DateTime.Now.AddDays(-10),
				Value = -2000
			},
		};*/

		private readonly OOPBankMultiuserContext _context = new();

		public MovementRepository(OOPBankMultiuserContext DBContext)
		{
			_context = DBContext;
		}

		public List<Movement> GetMovements(int currentAccountNumber)
		{
			return _context.Movements.Where(x => x.AccountId == currentAccountNumber).ToList();
		}

		public void AddMovement(Movement newMovement)
		{
			_context.Movements.Add(newMovement);
		}
	}
}

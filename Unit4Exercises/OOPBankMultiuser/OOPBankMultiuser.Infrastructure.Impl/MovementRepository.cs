using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Contracts.Data;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;

namespace OOPBankMultiuser.Infrastructure.Impl
{
	public class MovementRepository : IMovementRepository
	{
		

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

using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;

namespace OOPBankMultiuser.Infrastructure.Impl
{
	public class MovementRepository : IMovementRepository
	{
		private static List<MovementEntity> simulatedMovementsDBTable = new();

		public List<MovementEntity> GetMovements()
		{
			return simulatedMovementsDBTable;
		}

		public void AddMovement(MovementEntity newMovement)
		{
			simulatedMovementsDBTable.Add(newMovement);
		}
	}
}

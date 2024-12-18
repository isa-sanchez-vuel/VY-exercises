﻿using OOPBankMultiuser.Infrastructure.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OOPBankMultiuser.Infrastructure.Contracts
{
	public interface IMovementRepository
	{
		List<MovementEntity> GetMovements(string currentAccountNumber);
		void AddMovement(MovementEntity movement);
	}
}

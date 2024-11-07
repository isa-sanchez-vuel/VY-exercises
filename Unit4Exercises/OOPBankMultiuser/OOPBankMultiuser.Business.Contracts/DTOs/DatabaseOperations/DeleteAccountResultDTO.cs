using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;
using OOPBankMultiuser.XCutting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPBankMultiuser.Application.Contracts.DTOs.DatabaseOperations
{
	public class DeleteAccountResultDTO
	{
		public bool HasErrors { get; set; }
		public DeleteAccountErrorEnum? Error { get; set; }
		public AccountDTO DeletedAccount { get; set; }
	}
}

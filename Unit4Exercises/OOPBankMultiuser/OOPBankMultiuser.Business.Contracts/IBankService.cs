﻿using OOPBankMultiuser.Application.Contracts.DTOs.BankOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;

namespace OOPBankMultiuser.Application.Contracts
{
    public interface IBankService
	{
		void SetCurrentAccount(AccountDTO account);
		CreateAccountResultDTO CreateAccount(string ownerName, string accountNumber, string pin);
		LoginResultDTO LoginAccount(string accountNumber, string pin);
		AccountDTO? GetCurrentAccount();

	}
}

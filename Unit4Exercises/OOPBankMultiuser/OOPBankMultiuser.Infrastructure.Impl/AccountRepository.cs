﻿using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Contracts.Data;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;

namespace OOPBankMultiuser.Infrastructure.Impl
{
	public class AccountRepository : IAccountRepository
	{
		

		private readonly OOPBankMultiuserContext _context;

		public AccountRepository(OOPBankMultiuserContext DBContext)
		{
			_context = DBContext;
		}

		public bool AccountExists(int id)
		{
			return (_context.Accounts?.Any(e => e.IdNumber == id)).GetValueOrDefault();
		}

		public List<Account>? GetAllAccounts()
		{
			return _context.Accounts.ToList();
		}
		public Account? GetAccountInfo(int accountNumber)
		{
			return _context.Accounts
				.FirstOrDefault(acc => acc.IdNumber == accountNumber);
		}

		public Account? AddAccount(Account newEntity)
		{
			_context.Accounts.Add(newEntity);
			_context.Accounts.SaveChanges();

			return newEntity;
		}

		public bool UpdateAccount(Account updatedEntity)
		{
			Account? entity = _context.Accounts
				.Find(updatedEntity.IdNumber);

			if (entity != null)
			{
				entity.Name = updatedEntity.Name;
				entity.Balance = updatedEntity.Balance;
				entity.Pin = updatedEntity.Pin;
				entity.AccountNumber = updatedEntity.AccountNumber;
				entity.Iban = updatedEntity.Iban;
				_context.Accounts.SaveChanges();

				return true; //operation success
			}
			else return false; //operation failure
		}

		public bool DeleteAccount(Account entity)
		{
			_context.Accounts.Remove(entity);
			_context.SaveChanges();

			if(!AccountExists(entity.IdNumber)) return true;
			return false;
		}
	}
}

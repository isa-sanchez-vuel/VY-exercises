using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2_OOPMultiBankAccount
{
	internal class Account
	{
		string OwnerName;
		string Iban;

		string AccountNumber;
		string Pin;

		decimal TotalBalance;

		List<Movement> Movements = new();

		public Account(string id, string pin, string owner) 
		{
			TotalBalance = 0;
			AccountNumber = id;
			this.Pin = pin;
			OwnerName = owner;
		}

		public void CreateIban(string country, string bankId, string bankControl, string sucursal)
		{
			Iban = country + bankControl + bankId + sucursal + bankControl + AccountNumber;
		}

		public string GetId()
		{
			return AccountNumber;
		}

		public string GetPin()
		{
			return Pin;
		}

		public string GetName()
		{
			return OwnerName;
		}

		public string GetIban()
		{
			return AccountNumber;
		}

		public decimal? GetTotalMoney()
		{
			return TotalBalance;
		}


		public void AddIncome(decimal income)
		{
			TotalBalance += income; 
		}


		public void SubtractOutcome(decimal income)
		{
			TotalBalance -= income;
		}

		public List<Movement> GetAllMovements()
		{
			return Movements;
		}

		public void AddMovement(string content, string type)
		{
			Movements.Add(new Movement(type, content));
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2_OOPMultiBankAccount
{
    internal class Account
    {
        string ownerName;
        string iban;

        string accountNumber;
        string pin;

        decimal totalMoney;

        List<Movement> movements = new();

        public Account(string id, string pin, string owner) 
        {
            accountNumber = id;
            this.pin = pin;
            ownerName = owner;
        }

        public void CreateIban(string country, string bankId, string bankControl, string sucursal)
        {
            iban = country + bankControl + bankId + sucursal + bankControl + accountNumber;
        }

        public string GetId()
        {
            return accountNumber;
        }

        public string GetPin()
        {
            return accountNumber;
        }

        public string GetName()
        {
            return ownerName;
        }

        public string GetIban()
        {
            return accountNumber;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2_OOPMultiBankAccount
{
    internal class Bank
    {
        string entityName;
        string bankId;
        string controlNum;
        string officeId;
        string countryCode;
        List<Account> accounts = new();


        public Bank(string name, string country, string id, string control, string officeNumber) 
        {
            entityName = name;
            bankId = id;
            controlNum = control;
            officeId = officeNumber;
            countryCode = country;
        }

        public bool CreateAccount(string acId, string pin, string ownerName)
        {
            Account account = new(acId, pin, ownerName);
            account.CreateIban(countryCode, bankId, controlNum, officeId);
            accounts.Add(account);

            return true;
        }

        public Account CheckAccount(string accountId, string pin)
        {
            if (accountId.Length == 10 && pin.Length == 4)
            {
                foreach (Account account in accounts)
                {
                    if (accountId.Equals(account.GetId()) && pin.Equals(account.GetPin())) return account;
                }
            }
            return null;
        }
    }
}

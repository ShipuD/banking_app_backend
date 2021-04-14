using BankingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.BussinessLogic
{
   
    public class CreateAccount : ICommandTransaction
    {
        private readonly IAccount _account;   
        private readonly BankDBContext _context;
        
        public bool IsCommandCompleted
        {
            get; set;
        }
        public CreateAccount(IAccount account,  BankDBContext context)
        {
            _account = account;
            _context = context;

            IsCommandCompleted = false;
        }
        public async Task ExecuteCommand()
        {
            //Create a new account
            if (!string.IsNullOrEmpty(_account.AccountNo) && 
                !string.IsNullOrEmpty(_account.CustomerName) &&
                !string.IsNullOrEmpty(_account.LocalCurrencyCode))
            {
                _account.AccountBalance = 0.000;

                _context.Accounts.Add((Account)_account);                
                _context.SaveChanges();
                IsCommandCompleted = true;
            }
            else
            {
                throw new Exception(string.Format("Could not create account for :{0} as there may not be accountno or name or local curreny code", _account.CustomerName));
            }            
        }
    }
  }

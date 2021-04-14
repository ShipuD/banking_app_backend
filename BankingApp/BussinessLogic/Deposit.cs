using BankingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.BussinessLogic
{
    public class Deposit :ICommandTransaction
    {
        private readonly IAccount _account;
        private  double _amount;
        private readonly string _currencyCode;
        private readonly string _reference;
        private readonly BankDBContext _context;
        private IExchangeRate _exchangeRate;

        public bool IsCommandCompleted
        {
            get;set;
        }
        //With exchange rate
        public Deposit(Account account, double amount,string currencyCode,string reference,BankDBContext context, IExchangeRate exchangeRate)
        {
            _account = account;
            _amount = amount;
            _currencyCode = currencyCode;
            _context = context;
            _reference = reference;
            _exchangeRate = exchangeRate;

            IsCommandCompleted = false;
        }
        //Without exchange rate
        public Deposit(Account account, double amount, string currencyCode, string reference, BankDBContext context)
        {
            _account = account;
            _amount = amount;
            _currencyCode = currencyCode;
            _context = context;
            _reference = reference;
            IsCommandCompleted = false;
        }
        public async Task ExecuteCommand()
        {
            //Check if the currency of deposit is not same as Account curency
            if (_currencyCode != _account.LocalCurrencyCode)
            {
                double rate = await _exchangeRate.GetExchangeRate(_account.LocalCurrencyCode, _currencyCode);
                _amount *= rate;
            }
            
            _account.AccountBalance += _amount;

            Transaction transaction = new Transaction
            {
                account = (Account)_account,
                DateTime = DateTime.Now,
                Credit = _amount,
                Reference = _reference,
                Balance = _account.AccountBalance,
            };           
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            IsCommandCompleted = true;
        }
    }
}

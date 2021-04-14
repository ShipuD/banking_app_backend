using BankingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.BussinessLogic
{
    /// <summary>
    /// Command:Withdraw
    /// </summary>
    public class Withdraw:ICommandTransaction
    {
        private readonly Account _account;
        private readonly double _amount;
        private readonly string _reference;
        private readonly BankDBContext _context;

        public bool IsCommandCompleted
        {
            get; set;
        }
        public Withdraw(Account account, double amount, string reference, BankDBContext context)
        {
            _account = account;
            _amount = amount;
            _context = context;
            _reference = reference;
            IsCommandCompleted = false;
        }
        public async Task ExecuteCommand()
        {
            if (_account.AccountBalance >= _amount)
            {
                _account.AccountBalance -= _amount;
                Transaction transaction = new Transaction()
                {
                    account = _account,
                    DateTime = DateTime.Now,
                    Debit   = _amount,
                    Reference = _reference,
                    Balance = _account.AccountBalance,
                };

                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                IsCommandCompleted = true;
            }
            else
            {
                throw new Exception(string.Format(ErrorMessages.OVERDRAFTERROR,_amount));
            }
        }
    }
}

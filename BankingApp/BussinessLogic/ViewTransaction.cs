using BankingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.BussinessLogic
{
    public class ViewTransaction: ICommandTransactionWithResult<List<Transaction>>
    {
        public List<Transaction> Result { get; private set; }
        private readonly Account _account;
        private readonly DateTime _fromDate;
        private readonly DateTime _toDate;
        private readonly BankDBContext _context;

        public bool IsCommandCompleted
        {
            get; set;
        }

        public ViewTransaction(Account account, DateTime fromDate, DateTime toDate,BankDBContext context)
        {
            _account = account;
            _fromDate = fromDate;
            _toDate = toDate;
            _context = context;
            IsCommandCompleted = false;

        }
        public async Task ExecuteCommand()
        {
            //Get the Transactions between fromDT and ToDate from EF 
            var trans =  _context.Transactions.Where(r => r.account.AccountNo == _account.AccountNo
                                        && r.DateTime >= _fromDate
                                        && r.DateTime <= _toDate);

            this.Result =  trans.ToList();          
            IsCommandCompleted = true;
        }
    }
}

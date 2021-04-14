using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.BussinessLogic
{
    public interface IAccount
    {
        public string AccountNo
        {
            get; set;
        }
        public string CustomerName
        {
            get; set;
        }
        public string LocalCurrencyCode
        {
            get; set;
        }
        public double AccountBalance
        {
            get; set;
        }
        public double GetCurrentBalance();
       // public List<Transaction> GetLatestTransactions();
        //public List<Transaction> ExportLatestTransactions(DateTime fromDt,DateTime toDt);
    }
}

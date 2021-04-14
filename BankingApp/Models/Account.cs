using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.BussinessLogic
{
    /// <summary>
    /// Reciver of the command
    /// </summary>
    public class Account:   IAccount
    {
       
        private string _accountNo;
        private string _customerName;
        private string _localCurrencyCode;
        private double _accountBalance;
        [Key]
        [StringLength(5)]
        public string AccountNo  
        {
            get => _accountNo;
            set => _accountNo = value;
        }
        [Required]
        [StringLength(150)]
        public string CustomerName 
        {
            get => _customerName;
            set => _customerName = value;
        }
        [Required]
        [StringLength(3)]
        public string LocalCurrencyCode  
        {
            get => _localCurrencyCode;
            set => _localCurrencyCode = value;
        }

        public double AccountBalance
        {
            get => _accountBalance;
            set => _accountBalance = value;
        }

        //public Account(IAccount account)
        //{
        //    _account = account;
        //}

        public double GetCurrentBalance()
        {
            return AccountBalance;
        }
    }
}

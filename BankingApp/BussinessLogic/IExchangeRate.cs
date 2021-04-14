using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.BussinessLogic
{
    public interface IExchangeRate
    {
        public Task<double> GetExchangeRate(string BaseCurrencyCode, string ToCurrencyCode);
    }
}

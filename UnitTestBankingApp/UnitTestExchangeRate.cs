using BankingApp.BussinessLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace UnitTestBankingApp
{
    [TestClass]
    public class UnitTestExchangeRate
    {
        public IExchangeRate _exchangeRate;
        public IConfiguration _config;

        [TestInitialize]
        public void Setup()
        {
            _config = program.InitConfiguration();
            _exchangeRate = new ExchangeRate(_config);
        }

        [TestMethod]
        public async Task GetTodaysExchangeRateFromGBPTOUSD()
        {
           double rate = await _exchangeRate.GetExchangeRate("GBP", "USD");
            Assert.IsTrue(rate > 0.000);
        }
        [TestMethod]
        public async Task GetTodaysExchangeRateFromGBPTOINR()
        {
            double rate = await _exchangeRate.GetExchangeRate("GBP", "INR");
            Assert.IsTrue(rate > 0.000);
        }
    }   
}

using BankingApp.BussinessLogic;
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestBankingApp
{
    [TestClass]
    public class UnitTestAccount
    {
        public Account _account;
        public IConfiguration _config;
        private BankDBContext _bankDBContext;
        private IExchangeRate _exchangeRate;

        [TestInitialize]
        public void Setup()
        {
             _config = program.InitConfiguration();

            //Account to create
            _account = new Account()
            {
                AccountNo = "AC345",
                CustomerName = "Smith1 Barens1",
                LocalCurrencyCode = "GBP"
            };

            //Db Set up

            var connection = _config["ConnectionString:BankDb"];
            var builder = new DbContextOptionsBuilder<BankDBContext>()
                .UseSqlServer(connection);

            _bankDBContext = new BankDBContext(builder.Options);

            //Delete If exists
            ClearDbData();
            //Set up DB
            InitDBContext();
            _exchangeRate = new ExchangeRate(_config);
    
              //_bankDBContext.Accounts               
        }

        [TestMethod]
        public async Task CreateAccount()
        {
            //var model = PrepareDB();
            //Delete if already exists
            ClearDbData();
            CreateAccount ca = new CreateAccount(_account, _bankDBContext);
            await ca.ExecuteCommand();
            var dbBankAccount = _bankDBContext.Accounts.Select(r => r.AccountNo == _account.AccountNo);
            Assert.IsTrue(ca.IsCommandCompleted && dbBankAccount.Count() == 1);
           
        }
        [TestMethod]
        public async Task DespoistToAccountWithLocalCurrency()
        {
            ClearDbData();
            //Add account to update
            CreateAccount ca = new CreateAccount(_account, _bankDBContext);
            await ca.ExecuteCommand();
            
            double amount = 250.0;
            Deposit depoist = new Deposit(_account, amount,"GBP","Added gbp funds",_bankDBContext);
            await depoist.ExecuteCommand();
            Assert.IsTrue(_account.AccountBalance == 250.0 && depoist.IsCommandCompleted);

        }

        [TestMethod]
        public async Task DespoistToAccountWithAnotherCurrency()
        {
            ClearDbData();
            CreateAccount ca = new CreateAccount(_account, _bankDBContext);
            await ca.ExecuteCommand();
            
            double amount = 250.0;
            Deposit depoist = new Deposit(_account, amount, "USD", "Added Usd funds", _bankDBContext, _exchangeRate);
            await depoist.ExecuteCommand();
            Assert.IsTrue(depoist.IsCommandCompleted && _account.AccountBalance > 250.0);           
            
        }
        [TestMethod]
        public async Task WithdrwalFromAccountLessThanBalance()
        {
            ClearDbData();
            CreateAccount ca = new CreateAccount(_account, _bankDBContext);
            await ca.ExecuteCommand();
            //Deposit 250
            double amount = 250.0;            
            Deposit depoist = new Deposit(_account, amount, "GBP", "Added GBP funds", _bankDBContext);
            await depoist.ExecuteCommand();
            double amountToWithdraw = 200.0;
            Withdraw withdraw = new Withdraw(_account, amountToWithdraw, "Funds withdrwan", _bankDBContext);
            await withdraw.ExecuteCommand();
            Assert.IsTrue(withdraw.IsCommandCompleted && _account.AccountBalance ==50);
        }
        [TestMethod]
        public async Task WithdrwalFromAccountOverdraftError()
        {
            ClearDbData();
            CreateAccount ca = new CreateAccount(_account, _bankDBContext);
            await ca.ExecuteCommand();
            //Deposit 250
            double amount = 250.0;
            Deposit depoist = new Deposit(_account, amount, "GBP", "Added GBP funds", _bankDBContext);
            await depoist.ExecuteCommand();
            double amountToWithdraw = 300.0;
            try
            {
                Withdraw withdraw = new Withdraw(_account, amountToWithdraw, "Funds withdrwan", _bankDBContext);
                await withdraw.ExecuteCommand();
            }
            catch(Exception ex)
            {
                string expectedError = string.Format(ErrorMessages.OVERDRAFTERROR, amountToWithdraw);

                Assert.IsTrue(expectedError == ex.Message);
            }
            
        }

        public void InitDBContext()
        {          
            var accounts = Enumerable.Range(1, 3)
                .Select(i => new Account { AccountNo = "BK10" + i.ToString(), CustomerName = $"Smith{i} Baren{i}",LocalCurrencyCode ="GBP",AccountBalance = 0.00 });
            _bankDBContext.Accounts.AddRange(accounts);
            int changed = _bankDBContext.SaveChanges();
            
        }
        public void ClearDbData()
        {
            //Remove transactions
            var  transcations = _bankDBContext.Transactions.Select(r => r);
           _bankDBContext.Transactions.RemoveRange(transcations);
            _bankDBContext.SaveChanges();

            //Remove accounts
            var accounts = _bankDBContext.Accounts.Select(r => r);
            _bankDBContext.RemoveRange(accounts);
            _bankDBContext.SaveChanges();
        }
    }
}

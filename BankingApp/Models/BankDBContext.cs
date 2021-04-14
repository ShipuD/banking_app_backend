using BankingApp.BussinessLogic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.Models
{
    public class BankDBContext:DbContext
    {
        public BankDBContext()
        {
        }

        public BankDBContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }   
}

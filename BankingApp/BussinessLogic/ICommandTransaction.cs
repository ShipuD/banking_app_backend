using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp
{
    /// <summary>
    /// Actions defined for command executer
    /// </summary>
    public interface ICommandTransaction
    {
        Task ExecuteCommand();
        bool IsCommandCompleted
        {
            get; set;
        }
    }
}

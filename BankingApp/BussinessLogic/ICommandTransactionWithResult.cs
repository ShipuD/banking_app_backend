using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.BussinessLogic
{
    interface ICommandTransactionWithResult<T>:ICommandTransaction
    {
        T Result { get; }
    }
}

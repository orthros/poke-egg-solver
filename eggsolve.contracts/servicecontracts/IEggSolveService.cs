using System;
using System.ServiceModel;
using eggsolve.contracts.datacontracts;

namespace eggsolve.contracts.servicecontracts
{
    [ServiceContract]
    public interface IEggSolveService
    {
        [OperationContract]
        //[WebGet("")]
        void SolveEggs(EggDataSet eggs);
    }
}

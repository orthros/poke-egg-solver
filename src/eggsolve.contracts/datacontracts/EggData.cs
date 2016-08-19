using System;
using System.Runtime.Serialization;

namespace eggsolve.contracts.datacontracts
{
    [DataContract]
    public class EggData
    {
        [DataMember]
        public int Distance { get; set; }
    }
}

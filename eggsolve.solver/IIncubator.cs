using System.Collections.Generic;

namespace eggsolve.solver
{
    interface IIncubator
    {
        void Use(Egg eggToUse);
        bool CanUse { get; }
        List<Egg> IncubatedEggs { get; }
        int TotalDistance { get; }
    }
}

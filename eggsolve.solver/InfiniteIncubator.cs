using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace eggsolve.solver
{
    [DebuggerDisplay("CanUse: {CanUse} IncubatedEggs: {IncubatedEggs.Count} Distance: {TotalDistance}")]
    class InfiniteIncubator : IIncubator
    {
        public static InfiniteIncubator Get()
        {
            return new InfiniteIncubator();
        }
        private List<Egg> UsedEggs { get; set; }

        private InfiniteIncubator()
        {
            UsedEggs = new List<Egg>();
        }

        public bool CanUse
        {
            get
            {
                return true;
            }
        }

        public List<Egg> IncubatedEggs
        {
            get
            {
                return UsedEggs;
            }
        }

        public void Use(Egg eggToUse)
        {
            this.UsedEggs.Add(eggToUse);
            eggToUse.Hatch();
        }

        public int TotalDistance
        {
            get
            {
                return this.IncubatedEggs.Select(y => y.EggDistance).Cast<int>().Sum();
            }
        }
    }
}

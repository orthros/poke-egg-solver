using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace eggsolve.solver
{
    [DebuggerDisplay("CanUse: {CanUse} IncubatedEggs: {IncubatedEggs.Count} Distance: {TotalDistance}")]
    class Incubator : IIncubator
    {
        private static readonly int MAX_USES = 3;

        public int Used { get; private set; }

        public Egg[] Eggs { get; private set; }

        public Incubator()
        {
            this.Used = 0;
            Eggs = new Egg[3];
        }

        public void Use(Egg eggToUse)
        {
            if (!CanUse) { throw new InvalidOperationException("Cant use this incubator"); }

            Eggs[Used] = eggToUse;
            eggToUse.Hatch();
            this.Used++;
        }


        public virtual bool CanUse
        {
            get
            {
                return Used < MAX_USES;
            }
        }

        public List<Egg> IncubatedEggs
        {
            get
            {
                return Eggs.Where(x=> x != null).ToList();
            }
        }

        public int TotalDistance
        {
            get
            {
                return IncubatedEggs.Select(y => y.EggDistance).Cast<int>().Sum();
            }
        }
    }
}

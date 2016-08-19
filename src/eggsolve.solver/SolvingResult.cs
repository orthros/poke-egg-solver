using System.Collections.Generic;

namespace eggsolve.solver
{
    public class SolvingResult
    {
        public SolvingResult(List<Distance> infiniteDistances, List<List<Distance>> incubatorsAndDistances, List<Distance> infeasibleDistances)
        {
            InfiniteDistances = infiniteDistances;
            IncubatorsAndDistances = incubatorsAndDistances;
            InfeasibleDistances = infeasibleDistances;
        }

        public List<Distance> InfiniteDistances { get; private set; }

        public List<List<Distance>> IncubatorsAndDistances { get; private set; }

        public List<Distance> InfeasibleDistances { get; private set; }
    }
}

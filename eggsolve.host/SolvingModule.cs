using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using eggsolve.solver;

namespace eggsolve.host
{
    public class SolvingModule : NancyModule
    {
        public SolvingModule()
        {
            var availableDistances = new HashSet<int>() { 2, 5, 10 };
            Func<int, List<int>, bool> isValidSet = new Func<int,List<int>, bool>((distance, eggs) =>
            {
                if(!availableDistances.Contains(distance) || !availableDistances.IsSupersetOf(eggs))
                {
                    return false;
                }
                return true;
            });

            Get("/solve/{distance:int}/{egg1:int}", parameters => {
                    var distInt = (int)parameters.distance;
                    var egg1Int = (int)parameters.egg1;

                    List<int> eggs = new List<int>(){ egg1Int };
                    if(!isValidSet(distInt,eggs))
                    {
                        return null;
                    }

                    return GetResults((Distance)distInt, eggs.Cast<Distance>().ToList());
                });

            Get("/solve/{distance:int}/{egg1:int}/{egg2:int}", parameters => {
                    var distInt = (int)parameters.distance;
                    var egg1Int = (int)parameters.egg1;
                    var egg2Int = (int)parameters.egg2;

                    var eggTmp = (int)parameters["egg2"];

                    List<int> eggs = new List<int>(){ egg1Int, egg2Int };
                    if(!isValidSet(distInt,eggs))
                    {
                        return null;
                    }

                    return GetResults((Distance)distInt, eggs.Cast<Distance>().ToList());
                });

        }



        private SolvingResult GetResults(Distance distance, List<Distance> eggs)
        {
            EggSolver solv = new EggSolver();
            return solv.Solve(distance, eggs.Select(x => new Egg(x)).ToList());
        }
    }
}

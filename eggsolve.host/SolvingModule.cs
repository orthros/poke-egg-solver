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
            Func<int, List<int>, bool> isValidSet = new Func<int, List<int>, bool>((distance, eggs) =>
             {
                 if (!availableDistances.Contains(distance) || !availableDistances.IsSupersetOf(eggs))
                 {
                     return false;
                 }
                 return true;
             });

            var distKey = "distance";
            var eggKeyBase = "egg{0}";
            for (int i = 1; i < 10; i++)
            {
                var func = new Func<dynamic, object>(x =>
                {
                    if (!x.ContainsKey(distKey)) { return null; }
                    var distance = (int)x[distKey];
                    var eggVals = new List<int>();
                    for (int j = 0; j < i; j++)
                    {
                        if (x.ContainsKey(string.Format(eggKeyBase, j)))
                        {
                            eggVals.Add((int)x[string.Format(eggKeyBase, j)]);
                        }
                    }

                    if (!isValidSet(distance, eggVals))
                    {
                        return null;
                    }

                    return GetResults((Distance)distance, eggVals.Cast<Distance>().ToList());
                });

                string getString = string.Format("/solve/distance/{0}/eggs/{1}",
                                                 string.Concat("{", distKey, ":int}"),
                                                 string.Join("/", Enumerable.Range(0, i).Select(x => string.Concat("{", string.Format(eggKeyBase, x), ":int}"))));

                //Console.WriteLine(getString);

                Get(getString, func);
            }
        }

        private SolvingResult GetResults(Distance distance, List<Distance> eggs)
        {
            return EggSolver.Solve(distance, eggs.Select(x => new Egg(x)).ToList());
        }
    }
}

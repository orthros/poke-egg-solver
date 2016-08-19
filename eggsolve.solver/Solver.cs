using System.Collections.Generic;
using System.Linq;
using System;

namespace eggsolve.solver
{
    public class EggSolver
    {
        public static SolvingResult Solve(Distance walkingDistance, List<Egg> eggs)
        {
            var maxDistance = eggs.Where(x => x.EggDistance <= walkingDistance)
                                  .Select(x => x.EggDistance)
                                  .Max();

            Console.WriteLine(string.Format("The longest egg walk is: {0}", maxDistance));

            var infinateIncubator = InfiniteIncubator.Get();
            List<IIncubator> incubatorsToUse = new List<IIncubator>();

            var validEggs = eggs.Where(x => x.EggDistance <= walkingDistance);
            var invalidEggs = eggs.Where(x => x.EggDistance > walkingDistance);

            Console.WriteLine(string.Format("There are {0} eggs that cannot be hatched on this walk.\n\t{1}",
                                            invalidEggs.Count(),
                                            string.Join("\n\t", invalidEggs.Select(x=> x.EggDistance)))
                             );

            validEggs.Where(x => !x.Hatched)
                     .Each(x =>
                     {
                         var currentIncubatorSet = incubatorsToUse.Where(y => y.CanUse)
                                                                  .Where(y => y.TotalDistance + x.EggDistance <= maxDistance);
                         var bestToUse = currentIncubatorSet.Where(y => y.TotalDistance + x.EggDistance == maxDistance)
                                                            .FirstOrDefault();
                         Console.WriteLine(string.Format("For a {0} Egg, the current available incubators are {1}.\n"+
                                                         "This means the one whose TotalDistance + our Egg's distance is equal to " +
                                                         "our Max Distance ({2}) is {3}",
                                                         x.EggDistance,
                                                         string.Join(",",currentIncubatorSet.Select(y => y.TotalDistance)),
                                                         maxDistance,
                                                         bestToUse
                                                         ));
                         if (bestToUse == null)
                         {
                             if(currentIncubatorSet.Any())
                             {                                 
                                 bestToUse = currentIncubatorSet.MinBy(y=> y.TotalDistance);
                             }
                         }
                         if (bestToUse == null)
                         {
                             bestToUse = new Incubator();
                             incubatorsToUse.Add(bestToUse);
                         }

                         bestToUse.Use(x);
                     });

            //So at this point we've "bin filled" as much as we can, throw everything else into the infinite

            validEggs.Where(x => !x.Hatched)
                     .Where(x => infinateIncubator.TotalDistance < (int)walkingDistance)
                     .Each(x =>
                     {
                         infinateIncubator.Use(x);
                     });

            //In theory, here we might be "all full" here. do ONE LAST PASS.
            //If our infinate incubator is going to walk our total distance already, then just continue
            //but if our infin incubator hasnt traveled far enough, pick the incubator out of our list of incubators
            //whose total distance + infin's current distance < walkingDistance & who has the MOST number of eggs
            if (infinateIncubator.TotalDistance < (int)walkingDistance)
            {
                var bestIncubatorToRemove = incubatorsToUse.Where(x => x.TotalDistance + infinateIncubator.TotalDistance <= (int)walkingDistance)
                                                           .MaxBy(x => x.IncubatedEggs.Count);
                if (bestIncubatorToRemove != null)
                {
                    bestIncubatorToRemove.IncubatedEggs.Each(x => infinateIncubator.Use(x));
                    incubatorsToUse.Remove(bestIncubatorToRemove);
                }
            }


            return new SolvingResult(infinateIncubator.IncubatedEggs.Select(x => x.EggDistance).ToList(),
                                     incubatorsToUse.Select(x => x.IncubatedEggs.Select(y => y.EggDistance).ToList()).ToList(),
                                     invalidEggs.Union(validEggs.Where(x => !x.Hatched)).Select(x=> x.EggDistance).ToList());
        }
    }
}

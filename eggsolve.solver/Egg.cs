using System.Diagnostics;

namespace eggsolve.solver
{
    [DebuggerDisplay("Distance: {EggDistance} Hatched: {Hatched}")]
    public class Egg
    {
        public Egg(Distance eggDistance)
        {
            EggDistance = eggDistance;
            Hatched = false;
        }

        public bool Hatched { get; private set; }

        public void Hatch() { this.Hatched = true; }

        public Distance EggDistance { get; private set; }

    }
}

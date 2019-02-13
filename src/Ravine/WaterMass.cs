using System;
using System.Diagnostics;

namespace MapMaker.Ravine
{

    [DebuggerDisplay("M:{Mass}, V({Speed.X},{Speed.Y})")]
    public class WaterMass
    {

        public double Mass { get; set; }

        public Vector Speed { get; set; }

        public double MudMass { get; set; }

        public WaterMass(double mass)
        {
            Mass = mass;
            Speed = new Vector(0, 0);
            MudMass = 0;
        }

        public static WaterMass operator +(WaterMass water1, WaterMass water2)
        {
            var result = new WaterMass(water1.Mass + water2.Mass)
            {
                Speed = water1.Speed + water2.Speed,
                MudMass = water1.MudMass + water2.MudMass
            };
            return result;
        }

    }
}

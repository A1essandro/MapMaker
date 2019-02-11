using System;

namespace MapMaker.Ravine
{
    public class WaterMass
    {

        public double Mass { get; private set; }

        [Obsolete]
        public Vector Position { get; private set; }

        public Vector Speed { get; private set; }

        public double MudMass { get; private set; }

        public WaterMass(double mass, Vector position)
        {
            Mass = mass;
            Position = position;
            Speed = new Vector(0, 0);
            MudMass = 0;
        }

        public WaterMass(double mass, int x, int y)
            : this(mass, new Vector(x, y))
        {

        }

        public void Divide()
        {

        }

        public void Move()
        {
            var x = (Speed.X / Math.Abs(Speed.Y)) / Math.Abs(Speed.X / Speed.Y); // must be in set {-1, 0, 1}
            var y = (Speed.Y / Math.Abs(Speed.X)) / Math.Abs(Speed.Y / Speed.X); // must be in set {-1, 0, 1}
            Position += new Vector(y, x);
        }

        public static WaterMass operator +(WaterMass water1, WaterMass water2)
        {
            var result = new WaterMass(water1.Mass + water2.Mass, water1.Position);
            result.Speed = water1.Speed + water2.Speed;
            result.MudMass = water1.MudMass + water2.MudMass;

            return result;
        }

    }
}

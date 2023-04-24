using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : IBall
    {

        public Ball(Vector2 position) {
            Mass = 10;
            Radius = 20;
        this.Position = position;
        }

        //implementacja interface
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Mass { get; set; }
        public float Speed { get; set; }
        public float Radius { get; set; }
       
    }
}

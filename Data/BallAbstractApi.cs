using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class BallAbstractApi
    {
        public abstract void Add(IBall ball);
        public abstract IBall Get(int index);
        public abstract void Remove(IBall ball);
        public abstract int GetBallNumber();
        public static BallAbstractApi CreateList() { return new BallList(); }
        public static IBall CreateBall(Vector2 position) { return new Ball(position); }
    }
}

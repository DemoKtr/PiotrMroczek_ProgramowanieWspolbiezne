using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class BallList : BallAbstractApi
    {
        private List<IBall> ballList;
        public BallList()
        {
            this.ballList = new List<IBall>();
        }

        public override void Add(IBall ball)
        {
            ballList.Add(ball);
        }

        public override IBall Get(int index)
        {
            return ballList[index];
        }

        public override int GetBallNumber()
        {
            return ballList.Count;
        }

        public override float GetMass()
        {
            return 10;
        }

        public override float GetRadius()
        {
            return 20;
        }

        //Na przyszlosc
        public override void Remove(IBall ball)
        {
            throw new NotImplementedException();
        }
    }
}

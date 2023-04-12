using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class BallCollection : BallColletionApi
    {
        List<LogicAbstractApi> ballCollection;
        public override void CreateBallCollection(int size)
        {
            ballCollection = new List<LogicAbstractApi>();
            for (int i = 0; i < size; i++)
            {
                Logic ball = new Logic();
                ballCollection.Add(ball.CreateBall());
            }
        }

        public override List<LogicAbstractApi> GetBallsCollection()
        {
            return ballCollection;
        }
    }
}

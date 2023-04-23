using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class BallLogic : BallLogicAPI

    {
        public  float Radius;
        public  float Mass;
        private readonly BallAbstractApi dataBalls;
        public Vector2 BoardSize { get; }
        public BallLogic(BallAbstractApi dataBalls, Vector2 boardSize)
        {
            this.dataBalls = dataBalls;
            BoardSize = boardSize;
            Radius = dataBalls.GetRadius();
            Mass = dataBalls.GetMass();
           
        }

        public override void AddBall(Vector2 position)
        {
            
            dataBalls.Add(BallAbstractApi.CreateBall(position));
        }

        public override IList<Ilogic> GetBalls()
        {
            throw new NotImplementedException();
        }

        public override int GetBallsNumber()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            for (int i = 0; i < dataBalls.GetBallNumber(); i++)
            {
                
            }
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public override void AddBalls(int amount)
        {
            throw new NotImplementedException();
        }
    }
}

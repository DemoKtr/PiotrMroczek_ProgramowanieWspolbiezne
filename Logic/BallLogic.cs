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
        public CancellationTokenSource CancelSimulationSource { get; private set; }
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
            if (position.X < 0 || position.X > BoardSize.X || position.Y < 0 || position.Y > BoardSize.Y) { }
            else { dataBalls.Add(BallAbstractApi.CreateBall(position)); }
                
        }

        public override List<BallLogicAPI> GetBalls()
        {
            List<BallLogicAPI> ballsList = new List<BallLogicAPI>();
            for (var i = 0; i < dataBalls.GetBallNumber(); i++) ballsList.Add(new BallLogic(dataBalls, this.BoardSize));

            return ballsList;
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
            CancelSimulationSource.Cancel();
        }

        public override void AddBalls(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var randomPoint = GetRandomPointInsideBoard();
                dataBalls.Add(BallAbstractApi.CreateBall(randomPoint));
            }
        }
        private Vector2 GetRandomPointInsideBoard()
        {
            var rng = new Random();
            var x = rng.Next((int)Radius, (int)(BoardSize.X - Radius));
            var y = rng.Next((int)Radius, (int)(BoardSize.Y - Radius));

            return new Vector2(x, y);
        }
    }
}

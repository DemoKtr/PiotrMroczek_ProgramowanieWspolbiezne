using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Data;
using Logic;

namespace Model
{
    public class Model
    {
        private readonly Vector2 boardSize;
        private int ballsAmount;
        private BallLogicAPI ballsLogic;

        public event EventHandler<AdapterEventArgs>? BallPositionChange;

        public Model()
        {
            boardSize = new Vector2(650, 400);
            ballsAmount = 0;
            ballsLogic = BallLogicAPI.CreateBallsLogic(boardSize);
            ballsLogic.PositionChange += (sender, args) =>
            {
                BallPositionChange?.Invoke(this, new AdapterEventArgs(args.Ball.Position, args.Ball.Id));
            };
        }
        public void StartSimulation()
        {
            ballsLogic.AddBalls(ballsAmount);
            ballsLogic.Start();
        }

        public void StopSimulation()
        {
            ballsLogic.Stop();
            ballsLogic = BallLogicAPI.CreateBallsLogic(boardSize);
            ballsLogic.PositionChange += (sender, args) =>
            {
                BallPositionChange?.Invoke(this, new AdapterEventArgs(args.Ball.Position, args.Ball.Id));
            };
        }

        public void SetBallNumber(int amount)
        {
            ballsAmount = amount;
        }

        public int GetBallsCount()
        {
            return ballsAmount;
        }

        public void OnBallPositionChange(AdapterEventArgs args)
        {
            BallPositionChange?.Invoke(this, args);
        }
    }
}

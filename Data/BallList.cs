﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class BallList : BallAbstractApi
    {

        private const int MaxStartSpeed = 200;
        private const int MinStartSpeed = 100;
        private const int MinRadius = 25;
        private const int MaxRadius = 30;
        private readonly List<IBall> ballsList;
        private readonly IBallListLogger ballListLogger = new BallListLogger();

        public BallList(Vector2 boardSize) : base(boardSize)
        {
            ballsList = new List<IBall>();
        }
        public override void Add(int howMany)
        {
            var rand = new Random();
            for (var i = 0; i < howMany; i++)
            {
                var radius = rand.Next(MinRadius, MaxRadius);
                var weight = rand.Next(25, 50);

                var position = this.GetRandomPointInsideBoard(radius);
                var velocity = this.GetRandomVelocity();
                IBall ball = new Ball(ballsList.Count, position, radius, weight, velocity, this);

                ballsList.Add(ball);
            }
        }

        public override void Start()
        {
            if (CancelSimulationSource.IsCancellationRequested)
            {
                return;
            }

            foreach (var ball in ballsList)
            {
                ball.PositionChange += this.OnBallOnPositionChange;

                Task.Factory.StartNew(ball.Simulate, CancelSimulationSource.Token);
            }
        }

        public override void Stop()
        {
            CancelSimulationSource.Cancel();
        }

        private Vector2 GetRandomPointInsideBoard(int ballRadius)
        {
            var rng = new Random();
            var isPositionCorrect = false;
            var x = 0;
            var y = 0;
            var i = 0;
            while (!isPositionCorrect)
            {
                x = rng.Next(ballRadius, (int)(BoardSize.X - ballRadius));
                y = rng.Next(ballRadius, (int)(BoardSize.Y - ballRadius));

                isPositionCorrect = this.CheckIsSpaceFree(new Vector2(x, y), ballRadius);

               
                i++;
            }

            return new Vector2(x, y);
        }
        private void OnBallOnPositionChange(object? sender, OnBallPositionChangeEventArgs args)
        {
           // ballListLogger.AddToLogQueue(args.Ball);
            var newArgs = new OnPositionChangeEventArgs(args.Ball, new List<IBall>(ballsList));
            this.OnPositionChange(newArgs);
        }
        private Vector2 GetRandomVelocity()
        {
            var rng = new Random();
            var x = rng.Next(-MaxStartSpeed, MaxStartSpeed);
            var y = rng.Next(-MaxStartSpeed, MaxStartSpeed);
            if (Math.Abs(x) < MinStartSpeed)
            {
                x = MinStartSpeed;
            }

            if (Math.Abs(y) < MinStartSpeed)
            {
                y = MinStartSpeed;
            }

            return new Vector2(x, y);
        }


        private bool CheckIsSpaceFree(Vector2 position, int ballRadius)
        {
            foreach (var ball in ballsList)
            {
                if (this.IsCirclesCollides(ball.Position, ball.Radius, position, ballRadius))
                {
                    return false;
                }
            }

            return true;
        }
        private bool IsCirclesCollides(Vector2 position1, float radius1, Vector2 position2, float radius2)
        {
            var distSq = (position1.X - position2.X) * (position1.X - position2.X) + (position1.Y - position2.Y) * (position1.Y - position2.Y);
            var radSumSq = (radius1 + radius2) * (radius1 + radius2);
            return distSq <= radSumSq;
        }
    }
}

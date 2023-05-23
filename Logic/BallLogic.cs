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
        
        private readonly Mutex simulationPauseMutex = new(false);
        private readonly BallAbstractApi dataBalls;
       
        
        public BallLogic(BallAbstractApi dataBalls)
        {
            this.dataBalls = dataBalls;
 

        }

        public override void AddBalls(int howMany)
        {
            dataBalls.Add(howMany);

        }
  
        public override void Start()
        {

#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            dataBalls.PositionChange += this.OnDataBallsOnPositionChange;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            dataBalls.Start();
        }

        private void OnDataBallsOnPositionChange(object _, Data.OnPositionChangeEventArgs args)
        {
            this.HandleBallsCollisions(args.SenderBall, args.Balls);
            CollisionHandler.CollideWithWalls(args.SenderBall, dataBalls.BoardSize);
            var newArgs = new OnPositionChangeEventArgs(new LogicBallAdapter(args.SenderBall));
            this.OnPositionChange(newArgs);
        }


        private void HandleBallsCollisions(IBall ball, IList<IBall> allBalls)
        {
            simulationPauseMutex.WaitOne();
            try
            {
                var collidedBall = CollisionHandler.CheckCollisions(ball, allBalls);
                if (collidedBall != null)
                {
                    CollisionHandler.HandleCollision(ball, collidedBall);
                }
            }
            finally
            {
                simulationPauseMutex.ReleaseMutex();
            }
        }

        public override void Stop()
        {
            dataBalls.Stop();
        }


      
    }

    internal class CollisionHandler
    {
        public static IBall? CheckCollisions(IBall ball, IEnumerable<IBall> ballsList)
        {
            foreach (var ballTwo in ballsList)
            {
                if (ReferenceEquals(ball, ballTwo))
                {
                    continue;
                }

                if (AreBallSColliding(ball, ballTwo))
                {
                    return ballTwo;
                }
            }

            return null;
        }

        private static bool AreBallSColliding(IBall ballOne, IBall ballTwo)
        {
            var centerOne = GetBallCenterWithVelocity(ballOne);
            var centerTwo = GetBallCenterWithVelocity(ballTwo);

            var distance = Vector2.Distance(centerOne, centerTwo);
            var radiusSum = (ballOne.Radius + ballTwo.Radius) / 2f;

            return distance <= radiusSum;
        }
        private static Vector2 GetBallCenterWithVelocity(IBall ball)
        {
            return ball.Position + (Vector2.One * ball.Radius / 2) + ball.Velocity * GetTimeDelta();
        }
        public static void CollideWithWalls(IBall ball, Vector2 boardSize)
        {
            var position = ball.Position + ball.Velocity * GetTimeDelta();
            if (position.X <= 0 || position.X + ball.Radius >= boardSize.X)
            {
                ball.Velocity = new Vector2(-ball.Velocity.X, ball.Velocity.Y);
            }

            if (position.Y <= 0 || position.Y + ball.Radius >= boardSize.Y)
            {
                ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
            }
        }

        public static void HandleCollision(IBall firstBall, IBall secondBall)
        {
            var centerOne = firstBall.Position + (Vector2.One * firstBall.Radius / 2);
            var centerTwo = secondBall.Position + (Vector2.One * secondBall.Radius / 2);

            var unitNormalVector = Vector2.Normalize(centerTwo - centerOne);
            var unitTangentVector = new Vector2(-unitNormalVector.Y, unitNormalVector.X);

            var velocityOneNormal = Vector2.Dot(unitNormalVector, firstBall.Velocity);
            var velocityOneTangent = Vector2.Dot(unitTangentVector, firstBall.Velocity);
            var velocityTwoNormal = Vector2.Dot(unitNormalVector, secondBall.Velocity);
            var velocityTwoTangent = Vector2.Dot(unitTangentVector, secondBall.Velocity);

            var newNormalVelocityOne = (velocityOneNormal * (firstBall.Mass - secondBall.Mass) + 2 * secondBall.Mass * velocityTwoNormal) / (firstBall.Mass + secondBall.Mass);
            var newNormalVelocityTwo = (velocityTwoNormal * (secondBall.Mass - firstBall.Mass) + 2 * firstBall.Mass * velocityOneNormal) / (firstBall.Mass + secondBall.Mass);

            var newVelocityOne = Vector2.Multiply(unitNormalVector, newNormalVelocityOne) + Vector2.Multiply(unitTangentVector, velocityOneTangent);
            var newVelocityTwo = Vector2.Multiply(unitNormalVector, newNormalVelocityTwo) + Vector2.Multiply(unitTangentVector, velocityTwoTangent);

            firstBall.Velocity = newVelocityOne;
            secondBall.Velocity = newVelocityTwo;
        }
        private static float GetTimeDelta()
        {
            return 16f / 1000f;
        }
    }
}

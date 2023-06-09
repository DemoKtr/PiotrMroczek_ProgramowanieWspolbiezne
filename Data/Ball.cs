﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : IBall
    {
        private readonly BallAbstractApi owner;
        Task task;
        public Ball(int ID, Vector2 position, float radius, float weight, Vector2 velocity, BallAbstractApi owner)
        {
            Position = position;
            Velocity = velocity;
            this.owner = owner;
            Mass = weight;
            Radius = radius;
            this.ID = ID;

        }
        public event EventHandler<OnBallPositionChangeEventArgs>? PositionChange;
        //implementacja interface
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; set; }
        public float Mass { get;}
        public float Radius { get; set; }
        public int ID { get; }

        public async Task Simulate()
        {
            var sw = new Stopwatch();
            var deltaTime = 0.001f;
            while (!owner.CancelSimulationSource.Token.IsCancellationRequested)
            {
                sw.Reset();
                sw.Start();
                var newArgs = new OnBallPositionChangeEventArgs(this);
                PositionChange?.Invoke(this, newArgs);
                
                var nextPosition = Position + Vector2.Multiply(Velocity, deltaTime);
                Position = this.ClampPosition(nextPosition);
                await Task.Delay(4 , owner.CancelSimulationSource.Token).ContinueWith(_ => { });
                sw.Stop();

                deltaTime = sw.ElapsedMilliseconds / 1000f ;
                
            }
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", ID);
            info.AddValue("Radius", Radius);
            info.AddValue("Mass", Mass);
            info.AddValue("Position", Position);
            info.AddValue("Velocity", Velocity);
        }
        public void CreateMovementTask()
        {
            
            task = Simulate();
        }
        private Vector2 ClampPosition(Vector2 nextPosition)
        {
            Vector2 boardLimit = new Vector2(owner.BoardSize.X - Radius + 1, owner.BoardSize.Y - Radius + 1);
            nextPosition.X = Math.Clamp(nextPosition.X, -1, boardLimit.X);
            nextPosition.Y = Math.Clamp(nextPosition.Y, -1, boardLimit.Y);
            return nextPosition;
        }

    }
}

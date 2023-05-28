using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Data
{
    public interface IBall  : ISerializable
    {
        Vector2 Position { get; }
        Vector2 Velocity { get; set; }
        float Mass { get;  }
        float Radius { get; }
        int ID { get; }
        public Task Simulate();

        public event EventHandler<OnBallPositionChangeEventArgs>? PositionChange;
       
    }
    public class OnBallPositionChangeEventArgs
    {
        public IBall Ball;

        public OnBallPositionChangeEventArgs(IBall ball)
        {
            this.Ball = ball;
        }
    }
    internal interface IBallListLogger
    {
        void AddToLogQueue(IBall ball);
    }
}

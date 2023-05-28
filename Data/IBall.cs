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
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", ID);
            info.AddValue("Radius", Radius);
            info.AddValue("Mass", Mass);
            info.AddValue("Position", Position);
            info.AddValue("Velocity", Velocity);
        }
    }
    public class OnBallPositionChangeEventArgs
    {
        public IBall Ball;

        public OnBallPositionChangeEventArgs(IBall ball)
        {
            this.Ball = ball;
        }
    }
}

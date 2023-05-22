using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface Ilogic
    {
        Vector2 Position { get;}
        float Radius { get; }
        int ID{ get; }
    }

   

    internal class LogicBallAdapter : Ilogic
    {
        private readonly IBall ball;

        public LogicBallAdapter(IBall ball)
        {
            this.ball = ball;
        }

        public Vector2 Position { get => ball.Position; }

        public float Radius { get => ball.Radius; }

        public int ID { get => ball.ID; }

        
    }

    public class OnPositionChangeEventArgs : EventArgs
    {
        public Ilogic Ball;

        public OnPositionChangeEventArgs(Ilogic ball)
        {
            this.Ball = ball;
        }
    }
}

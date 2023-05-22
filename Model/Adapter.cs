using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IModel
    {
        Vector2 Position { get; }
        float Radius { get; }
        int ID { get; }
    }

    public class ModelBallAdapter : IModel

    {
        private readonly Ilogic ball;


        public ModelBallAdapter(Ilogic ball)
        {
            this.ball = ball;
        }
        public Vector2 Position { get => ball.Position; }
        public float Radius { get => ball.Radius; }
        public int ID { get => ball.ID; }
    }
    public class OnPositionChangeEventArgs : EventArgs
    {
        public readonly IModel Ball;

        public OnPositionChangeEventArgs(IModel ball)
        {
            Ball = ball;
        }
    }
}

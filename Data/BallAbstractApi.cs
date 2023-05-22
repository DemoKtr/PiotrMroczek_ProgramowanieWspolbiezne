using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class BallAbstractApi
    {
        public Vector2 BoardSize { get; protected set; }
        public CancellationTokenSource CancelSimulationSource { get; }
        public event EventHandler<OnPositionChangeEventArgs>? PositionChange;
        protected BallAbstractApi(Vector2 boardSize)
        {
            this.BoardSize = boardSize;
            CancelSimulationSource = new CancellationTokenSource();
        }
        protected void OnPositionChange(OnPositionChangeEventArgs argv)
        {
            PositionChange?.Invoke(this, argv);
        }
        public abstract void Add(int howMany);


        public abstract void Start();
        public abstract void Stop();
        public static BallAbstractApi CreateList(Vector2 boardSize) { return new BallList(boardSize); }
    }

    public class OnPositionChangeEventArgs : EventArgs
    {
        public readonly IList<IBall> Balls;
        public readonly IBall SenderBall;

        public OnPositionChangeEventArgs(IBall senderBall, IList<IBall> balls)
        {
            this.Balls = balls;
            this.SenderBall = senderBall;
        }

    }
}

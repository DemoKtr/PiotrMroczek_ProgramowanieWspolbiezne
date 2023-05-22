using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public abstract class BallLogicAPI
    {
        public event EventHandler<OnPositionChangeEventArgs>? PositionChange;

        public abstract void AddBalls(int amount);

        public abstract void Start();
        public abstract void Stop();


        protected void OnPositionChange(OnPositionChangeEventArgs args)
        {
            PositionChange?.Invoke(this, args);
        }

        public static BallLogicAPI CreateBallsLogic(Vector2 boardSize, BallAbstractApi? dataApi = default(BallAbstractApi))
        {
            dataApi ??= BallAbstractApi.CreateList(boardSize);
            return new BallLogic(dataApi);
        }

    }
}

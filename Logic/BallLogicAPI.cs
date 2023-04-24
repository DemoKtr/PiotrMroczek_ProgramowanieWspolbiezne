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
        public event EventHandler<ChangePositionEventArgs>? PositionChange;
        public abstract void AddBall(Vector2 position);
        public abstract void AddBalls(int amount);
        
        public abstract void Start();
        public abstract void Stop();
        public abstract int GetBallsNumber();
        
        public abstract List<Ilogic> GetBalls();

        public static BallLogicAPI CreateBallsLogic(Vector2 boardSize, BallAbstractApi dataApi = default(BallAbstractApi))
        {
            if (dataApi == null)
            {
                dataApi = BallAbstractApi.CreateList();
            }
            return new BallLogic(dataApi, boardSize);
        }
        protected virtual void OnPositionChange(ChangePositionEventArgs args)
        {
            PositionChange?.Invoke(this, args);
        }

        
    }
}

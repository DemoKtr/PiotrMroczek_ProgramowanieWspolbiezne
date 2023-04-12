using System.Numerics;
using Data;
namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract LogicAbstractApi CreateBall();
        public abstract Vector2 PutBallOnBoard();
        public abstract Vector2 getBallPosition();
        public abstract void setBallPositionX(float XPos);
        public abstract void setBallPositionY(float YPos);
        public abstract Vector2 NextStepPosition(Vector2 currentPos, Vector2 nextPosition);
        public abstract DataAbstractAPI GetDataAPI();

        public static LogicAbstractApi CreateObjLogic(DataAbstractAPI data = default(DataAbstractAPI))
        {
            return new Logic();
        }
    }
}
using Logic
using Logic;
using System.Numerics;

namespace Model
{
    public class Model
    {
        private LogicAbstractApi logic;
        private double X;
        private double Y;

        public BallModel()
        {
            logic = LogicApi.CreateObjLogic();
            X = logic.getBallPosition().X;
            Y = logic.getBallPosition().Y;
        }

        public double ModelXPosition
        {
            get
            {
                return logic.getBallPosition().X;
            }
            set
            {
                logic.setBallXPosition(value);
            }
        }

        public double ModelYPosition
        {
            get
            {
                return logic.getBallPosition().Y;
            }
            set
            {
                logic.setBallYPosition(value);
            }
        }

        public Vector2 getModelPosition()
        {
            return new Vector2((float)ModelXPosition, (float)ModelYPosition);
        }

        public Vector2 GetBallPosition()
        {
            return logic.PutBallOnBoard();
        }
    }
}
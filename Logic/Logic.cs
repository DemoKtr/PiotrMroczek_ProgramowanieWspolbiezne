using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    internal class Logic : LogicAbstractApi
    {
        private Vector2 size = new Vector2(1000, 1000);
        private DataAbstractAPI data;

        public Logic()
        {
            Vector2 cords = PutBallOnBoard();
            data = DataAbstractAPI.CreateBall(cords.X, cords.Y);
        }

        public override DataAbstractAPI GetDataAPI()
        {
            return data;
        }


        public override Vector2 getBallPosition()
        {
            return new Vector2((float)data.getPositionX(), (float)data.getPositionY());
        }

        public override void setBallPositionX(float x)
        {
            data.setPositionX(x);
        }

        public override void setBallPositionY(float y)
        {
            data.setPositionY(y);
        }

        public override Vector2 PutBallOnBoard()
        {
            Random r = new Random();
            double x = r.NextDouble() * size.X;
            r = new Random();
            double y = r.NextDouble() * size.Y;
            y += 30;
            return new Vector2((float)x, (float)y);
        }

        public override Vector2 NextStepPosition(Vector2 position, Vector2 nextPosition)
        {
            Vector2 movement = nextPosition - position;
            return position + movement;
        }

        public override LogicAbstractApi CreateBall()
        {
            Vector2 coords = PutBallOnBoard();
            data = DataAbstractAPI.CreateBall(coords.X, coords.Y);
            return LogicAbstractApi.CreateObjLogic(data);
        }
    }
}

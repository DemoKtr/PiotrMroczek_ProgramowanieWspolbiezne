using Data;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Data
{
  internal class Data : DataAbstractAPI
    {
        private Vector2 position;
        public float radius=0.5f;

        public Data(float x, float y) { 
            this.position.X = x; this.position.Y = y;
        }

        public override float getPositionX()
        {
            return position.X;
        }
        public override float getPositionY() { 
            return position.Y;
        }
        public override void setPositionX(float x)
        {
            this.position.X = x;
        }
        public override void setPositionY(float y)
        {
            this.position.Y=y;
        }

       
    }
}
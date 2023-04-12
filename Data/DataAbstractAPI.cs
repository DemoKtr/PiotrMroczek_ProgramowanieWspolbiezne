using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public abstract class DataAbstractAPI
    {
        public abstract float getPositionX();
        public abstract float getPositionY();
        public abstract void setPositionX(float x);
        public abstract void setPositionY(float y);
        public static DataAbstractAPI CreateBall(float x, float y)
        {
            return new Data(x, y);
        }
    }
}

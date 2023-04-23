using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class ChangePositionEventArgs : EventArgs
    {
        public  Ilogic Ball;

        public ChangePositionEventArgs(Ilogic ball)
        {
            this.Ball = ball;
        }
    }
}

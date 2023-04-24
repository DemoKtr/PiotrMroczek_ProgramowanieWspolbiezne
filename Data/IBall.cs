using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IBall 
    {
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        float Mass { get; set; }
        float Speed { get; set; }
        float Radius { get; set; }
       
       


        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class AdapterEventArgs : EventArgs
    {
        public readonly Vector2 Position;
        public readonly int Id;

        public AdapterEventArgs(Vector2 position, int id)
        {
            this.Position = position;
            Id = id;
        }
    }
}

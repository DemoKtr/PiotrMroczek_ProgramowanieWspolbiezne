using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VM1
{
    internal class ViewModel
    {
    }




























    public class BallPosition : INotifyPropertyChanged
    {
        private Vector2 pos;
        public float X
        {
            get { return pos.X; }
            set { pos.X = value; OnPropertyChanged(); }
        }
        public float Y
        {
            get { return pos.Y; }
            set { pos.Y = value; OnPropertyChanged(); }
        }

        public BallPosition(float x, float y)
        {
            X = x;
            Y = y;
        }
        public BallPosition(Vector2 position)
        {
            X = position.X;
            Y = position.Y;
        }

        public BallPosition()
        {
            X = 0;
            Y = 0;
        }

        public void ChangePosition(Vector2 position)
        {
            this.X = position.X;
            this.Y = position.Y;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}

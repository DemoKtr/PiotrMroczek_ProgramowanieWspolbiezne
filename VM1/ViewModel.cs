using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VM1

{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private Model.Model model;
        public AsyncObservableCollection<BallPosition> Balls { get; set; }
        public ICommand AddButton { get; }
        public ICommand RemoveButton { get; }
        public ICommand StartButton { get; }
        public ICommand StopButton { get; }
        private bool bisSimulating=false;
        public ViewModel()
        {
            Balls = new AsyncObservableCollection<BallPosition>();

            model = new Model.Model();
            BallsCount = 0;

            AddButton = new RelayCommand(() =>
            {
                if(BallsCount < 10)
                BallsCount += 1;
            });
            RemoveButton = new RelayCommand(() =>
            {
                if (BallsCount > 0)
                    BallsCount -= 1;
            });

            StartButton = new RelayCommand(() =>
            {
                if (!bisSimulating) { 
                model.SetBallNumber(BallsCount);

                for (int i = 0; i < BallsCount; i++)
                {
                    Balls.Add(new BallPosition());
                }

                model.BallPositionChange += (sender, argv) =>
                {
                    if (Balls.Count > 0)
                        Balls[argv.Id].ChangePosition(argv.Position);
                };
                model.StartSimulation();
                    bisSimulating = true;
                }
            });

            StopButton = new RelayCommand(() =>
            {
                if (bisSimulating) { 
                model.StopSimulation();
                Balls.Clear();
                model.SetBallNumber(BallsCount);
                    bisSimulating=false;
                }

            });
        }
        public int BallsCount
        {
            get { return model.GetBallsCount(); }
            set
            {
                if (value >= 0)
                {
                    model.SetBallNumber(value);
                    OnPropertyChanged();
                }
            }
        }

        // Updates

        protected virtual void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
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
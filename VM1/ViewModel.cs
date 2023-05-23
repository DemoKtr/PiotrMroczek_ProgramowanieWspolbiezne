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
        public ISimpleCommand AddButton { get; }
        public ISimpleCommand RemoveButton { get; }
        public ISimpleCommand StartButton { get; }
        public ISimpleCommand StopButton { get; }

        public ViewModel()
        {
            Balls = new AsyncObservableCollection<BallPosition>();

            model = new Model.Model();
            BallsCount = 0;

            AddButton = new RelayCommand(() =>
            {

                BallsCount += 1;
            });
            RemoveButton = new RelayCommand(() =>
            {

                BallsCount -= 1;
            });

            StartButton = new RelayCommand(() =>
            {
                model.SetBallNumber(BallsCount);

                for (var i = 0; i < BallsCount; i++)
                {
                    Balls.Add(new BallPosition());
                }

                model.BallPositionChange += (sender, args) =>
                {
                    if (Balls.Count <= 0) return;

                    for (var i = 0; i < BallsCount; i++)
                    {
                        Balls[args.Ball.ID].Position = args.Ball.Position;
                        Balls[args.Ball.ID].Radius = args.Ball.Radius;
                    }
                };
                model.StartSimulation();
                this.ToggleSimulationButtons();
            });

            StopButton = new RelayCommand(() =>
            {
                model.StopSimulation();
                Balls.Clear();
                model.SetBallNumber(BallsCount);
                this.ToggleSimulationButtons();
            });
            StopButton.IsEnabled = false;
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
        private void ToggleSimulationButtons()
        {
            AddButton.IsEnabled = !AddButton.IsEnabled;
            RemoveButton.IsEnabled = !RemoveButton.IsEnabled;
            StartButton!.IsEnabled = !StartButton!.IsEnabled;
            StopButton!.IsEnabled = !StopButton!.IsEnabled;
        }
        // Updates

        protected virtual void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        public interface ISimpleCommand : ICommand
        {
            public bool IsEnabled { get; set; }
        }

        public class RelayCommand : ISimpleCommand
        {
            private readonly Action handler;
            private bool isEnabled;

            public RelayCommand(Action handler)
            {
                this.handler = handler;
                IsEnabled = true;
            }

            public bool IsEnabled
            {
                get { return isEnabled; }
                set
                {
                    if (value != isEnabled)
                    {
                        isEnabled = value;
                        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
            }

            public event EventHandler? CanExecuteChanged;

            public bool CanExecute(object? parameter)
            {
                return isEnabled;
            }

            public void Execute(object? parameter)
            {
                handler();
            }
        }







        public class BallPosition : INotifyPropertyChanged
        {

            private Vector2 pos;
            private float radius;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            public BallPosition()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            {
                this.pos = Vector2.Zero;
                this.radius = 0;
            }


            public Vector2? Position
            {
                get => pos;
                set
                {
                    this.X = value?.X ?? 0;
                    this.Y = value?.Y ?? 0;
                    this.OnPropertyChanged();
                }
            }

            public float Radius
            {
                get => radius;
                set
                {
                    radius = value;
                    this.OnPropertyChanged();
                }
            }
            public double X
            {
                get => pos.X;
                set
                {
                    pos.X = (float)value;
                    OnPropertyChanged();
                }
            }

            public double Y
            {
                get => pos.Y;
                set
                {
                    pos.Y = (float)value;
                    OnPropertyChanged();
                }
            }









#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
            public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
            private void OnPropertyChanged([CallerMemberName] string caller = "")
            {
                var args = new PropertyChangedEventArgs(caller);
                PropertyChanged?.Invoke(this, args);
            }
        }

    }
}
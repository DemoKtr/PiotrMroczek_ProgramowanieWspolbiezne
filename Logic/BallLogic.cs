using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class BallLogic : BallLogicAPI

    {
        public CancellationTokenSource CancelSimulationSource { get; private set; }
        public  float Radius;
        public  float Mass;
        private readonly BallAbstractApi dataBalls;
        public Vector2 BoardSize { get; }
        
        public BallLogic(BallAbstractApi dataBalls, Vector2 boardSize)
        {
            this.dataBalls = dataBalls;
            BoardSize = boardSize;
            Radius = 10;
            Mass = 5;
            CancelSimulationSource = new CancellationTokenSource();
            

        }

        public override void AddBall(Vector2 position)
        {
            if (position.X < 0 || position.X > BoardSize.X || position.Y < 0 || position.Y > BoardSize.Y) { }
            else { dataBalls.Add(BallAbstractApi.CreateBall(position)); }
                
        }

        public override List<Ilogic> GetBalls()
        {
            List<Ilogic> ballsList = new List<Ilogic>();
            for (var i = 0; i < dataBalls.GetBallNumber(); i++) ballsList.Add(new LogicBallDecorator(dataBalls.Get(i), i, this));

            return ballsList;
        }

        public override int GetBallsNumber()
        {
            return dataBalls.GetBallNumber();
        }

        public override void Start()
        {
            if (CancelSimulationSource.IsCancellationRequested) return;

            CancelSimulationSource = new CancellationTokenSource();
            for (int i = 0; i < dataBalls.GetBallNumber(); i++)
            {
                var ball = new LogicBallDecorator(dataBalls.Get(i), i, this);
                ball.PositionChange += (_, args) => OnPositionChange(args);
                Task.Factory.StartNew(ball.Simulate, CancelSimulationSource.Token);
            }
        }

        public override void Stop()
        {
            CancelSimulationSource.Cancel();
        }

        public override void AddBalls(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Vector2 randomPoint = this.GetRandomPointInsideBoard();
                dataBalls.Add(BallAbstractApi.CreateBall(randomPoint));
            }
        }
        private Vector2 GetRandomPointInsideBoard()
        {
            var rng = new Random();
            var x = rng.Next((int)Radius, (int)(BoardSize.X - Radius));
            var y = rng.Next((int)Radius, (int)(BoardSize.Y - Radius));

            return new Vector2(x, y);
        }

      
    }

    internal class LogicBallDecorator : Ilogic
    {
        private readonly IBall ball;
        private readonly BallLogic owner;
        private Random rng;
        public event EventHandler<ChangePositionEventArgs>? PositionChange;
        public int Id { get; private set; }
        Vector2 direction;
        private int frameCounter = 0;
        private int changeDirectionFrequency = 1000;

        public LogicBallDecorator(IBall ball, int id, BallLogic owner)
        {
            this.ball = ball;
            this.owner = owner;
            this.Id = id;
            rng = new Random();
            direction = GetRandomNormalizedVector();
        }

        public LogicBallDecorator(Vector2 position, int id, BallLogic owner)
        {
            ball = BallAbstractApi.CreateBall(position);
            this.Id = id;
            this.owner = owner;
            rng = new Random();
        }

        public Vector2 Position
        {
            get => ball.Position;
            set => ball.Position = value;
        }

        public async void Simulate()
        {
            while (!owner.CancelSimulationSource.Token.IsCancellationRequested)
            {
                Position = GetRandomPointInsideBoard();
                PositionChange?.Invoke(this, new ChangePositionEventArgs(this));

                
            }
        }

        private Vector2 GetRandomPointInsideBoard()
        {

            Vector2 newPosition = Position + direction/1000;

            if (newPosition.X <= owner.Radius || newPosition.X >= owner.BoardSize.X - owner.Radius)
            {
                direction = GetRandomNormalizedVector();
            }

            if (newPosition.Y <= owner.Radius || newPosition.Y >= owner.BoardSize.Y - owner.Radius)
            {
                direction = GetRandomNormalizedVector();
            }

            return newPosition;
        }


        private Vector2 GetRandomNormalizedVector()
        {
            var x = (float)(rng.NextDouble() - 0.5) * 2;
            var y = (float)(rng.NextDouble() - 0.5) * 2;
            var result = new Vector2(x, y);
            return Vector2.Normalize(result);
        }
    }
}

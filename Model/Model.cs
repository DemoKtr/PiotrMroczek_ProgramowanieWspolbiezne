﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Data;
using Logic;

namespace Model
{
    public class Model
    {
        private readonly Vector2 boardSize;
        private int ballsAmount;
        private BallLogicAPI ballsLogic;

        public event EventHandler<OnPositionChangeEventArgs>? BallPositionChange;

        public Model()
        {
            boardSize = new Vector2(650, 400);
            ballsAmount = 0;
            this.PrepareBallsLogic();
        }
        public void StartSimulation()
        {
            ballsLogic.AddBalls(ballsAmount);
            ballsLogic.Start();
        }

        public void StopSimulation()
        {
            ballsLogic.Stop();
            this.PrepareBallsLogic();
        }

        public void SetBallNumber(int amount)
        {
            ballsAmount = amount;
        }

        public int GetBallsCount()
        {
            return ballsAmount;
        }

        private void PrepareBallsLogic()
        {
            ballsLogic = BallLogicAPI.CreateBallsLogic(boardSize);
            ballsLogic.PositionChange += this.OnBallsLogicOnPositionChange;
        }
        private void OnBallsLogicOnPositionChange(object sender, Logic.OnPositionChangeEventArgs args)
        {
            BallPositionChange?.Invoke(this, new OnPositionChangeEventArgs(new ModelBallAdapter(args.Ball)));
        }
    }
}

﻿using System;
using Tetris.Model;
using Tetris.Model.Enumerators;
using Tetris.Signals;
using Zenject;

namespace Tetris.View
{
    public class GridInputTest : IInitializable, IDisposable
    {
        private GameGrid tetrisGrid;
        private SignalBus signalBus;

        public GridInputTest(GameGrid tetrisGrid, SignalBus signalBus)
        {
            this.signalBus = signalBus;
            this.tetrisGrid = tetrisGrid;
        }

        public void Initialize()
        {
            signalBus.Subscribe<GridInputMouseSignal>(OnMouseInput);
        }

        public void Dispose()
        {
            signalBus.Unsubscribe<GridInputMouseSignal>(OnMouseInput);
        }

        public void OnMouseInput(GridInputMouseSignal signal)
        {
            var colorValues = (MinoColorsEnum[])Enum.GetValues(typeof(MinoColorsEnum));
            MinoColorsEnum color = colorValues[UnityEngine.Random.Range(0, colorValues.Length)];
            if (tetrisGrid.GetCell(signal.Position) != null)
                tetrisGrid.DeleteMino(signal.Position);
            else
                tetrisGrid.CreateMino(signal.Position, color);
        }
    }
}

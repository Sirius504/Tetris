﻿using System;
using Tetris.Model;
using Tetris.Model.Enumerators;
using Tetris.Signals;
using UnityEngine;
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
            Debug.Log("shit");
            signalBus.Subscribe<GridInputMouseSignal>(OnMouseInput);
        }

        public void Dispose()
        {
            signalBus.Unsubscribe<GridInputMouseSignal>(OnMouseInput);
        }

        public void OnMouseInput(GridInputMouseSignal signal)
        {
            var colorValues = (CellColorsEnum[])Enum.GetValues(typeof(CellColorsEnum));
            CellColorsEnum color = colorValues[UnityEngine.Random.Range(0, colorValues.Length)];
            tetrisGrid.CreateCell(signal.Position, color);
        }
    }
}

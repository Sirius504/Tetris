﻿using System;
using Tetris.Model;
using Tetris.Signals;
using Zenject;

namespace Tetris.View
{
    public class SpawnTest : IInitializable, IDisposable
    {
        private TetraminoController tetrisGrid;
        private TetraminoFactory tetraminoFactory;
        private SignalBus signalBus;

        public SpawnTest(TetraminoController tetrisGrid, TetraminoFactory tetraminoFactory, SignalBus signalBus)
        {
            this.signalBus = signalBus;
            this.tetrisGrid = tetrisGrid;
            this.tetraminoFactory = tetraminoFactory;
        }

        public void Initialize()
        {
            signalBus.Subscribe<TetraminoLetterSignal>(OnTetraminoLetterInput);
        }

        public void Dispose()
        {
            signalBus.Unsubscribe<TetraminoLetterSignal>(OnTetraminoLetterInput);
        }

        public void OnTetraminoLetterInput(TetraminoLetterSignal signal)
        {
            var tetramino = tetraminoFactory.Create(signal.TetraminoType);
            tetrisGrid.Spawn(tetramino);
        }
    } 
}
using Tetris.Model;
using Tetris.Signals;
using Tetris.View;
using UnityEngine;
using Zenject;

namespace Tetris.Installers
{
    public class GridInstaller : MonoInstaller
    {
        [SerializeField]
        private Vector2Int gridSize;
        [SerializeField]
        private GameObject cellPrefab;
        [SerializeField]
        private Transform cellsParent;

        public override void InstallBindings()
        {
            var grid = new TetrisGrid(gridSize);
            Container.Bind<GameGrid>().FromInstance(grid).AsSingle();
            Container.BindInstance(grid).AsSingle();
            Container.BindSignal<TetraminoShiftSignal>()
                .ToMethod<TetrisGrid>((x, s) => x.ShiftTetramino(s.Shift))
                .FromResolve();
            Container.BindSignal<TetraminoRotationSignal>()
                .ToMethod<TetrisGrid>((x, s) => x.RotateTetramino(s.Rotation))
                .FromResolve();

            Container.Bind<TetraminoFactory>().AsSingle();

            Container.Bind<GridRenderer>().AsSingle();
            Container.BindMemoryPool<View.Cell, View.Cell.Pool>()
                .WithInitialSize(gridSize.x * gridSize.y / 2)
                .FromComponentInNewPrefab(cellPrefab)
                .UnderTransform(cellsParent);
        }
    }
}
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
            Container.Bind<GameGrid>().AsSingle().WithArguments(gridSize);
            Container.Bind<GridRenderer>().AsSingle();
            Container.BindMemoryPool<Cell, Cell.Pool>()
                .WithInitialSize(gridSize.x * gridSize.y / 2)
                .FromComponentInNewPrefab(cellPrefab)
                .UnderTransform(cellsParent);
        }
    }
}
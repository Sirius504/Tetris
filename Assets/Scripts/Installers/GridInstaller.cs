using Tetris.Model;
using Tetris.View;
using UnityEngine;
using Zenject;

namespace Tetris.Installers
{
    public class GridInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject cellPrefab;

        public override void InstallBindings()
        {
            Container.Bind<TetrisGrid>().AsSingle();
            Container.Bind<GridRenderer>().AsSingle();
            Container.BindMemoryPool<View.Cell, View.Cell.Pool>().FromComponentInNewPrefab(cellPrefab);
        }
    }
}
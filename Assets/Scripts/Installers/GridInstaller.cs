using Tetris.Model;
using Tetris.View;
using UnityEngine;
using Zenject;

public class GridInstaller : MonoInstaller
{
    [SerializeField]
    [HideInInspector]
    public Vector2Int GridSize = new Vector2Int(1, 1);
    public override void InstallBindings()
    {
        Container.Bind<TetrisGrid>().AsSingle().WithArguments(GridSize);
        Container.Bind<GridRenderer>().AsSingle();
    }
}
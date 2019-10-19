using Tetris.Model;
using Tetris.View;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "TetrisGridInstaller", menuName = "Installers/TetrisGridInstaller")]
public class TetrisGridInstaller : ScriptableObjectInstaller<TetrisGridInstaller>
{
    [SerializeField]
    private Vector2Int tetrisGridSize = default;

    public override void InstallBindings()
    {
        Container.Bind<TetrisGrid>().AsSingle().WithArguments(tetrisGridSize);
        Container.Bind<GridRenderer>().AsSingle();
    }
}
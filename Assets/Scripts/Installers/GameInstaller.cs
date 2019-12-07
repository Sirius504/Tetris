using Tetris.Model;
using Zenject;

namespace Tetris.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameMaster>()
                .AsSingle()
                .NonLazy();
        }
    }
}
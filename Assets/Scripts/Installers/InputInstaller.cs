using Tetris.Signals;
using Tetris.View;
using Zenject;

namespace Tetris.Installers
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.BindInterfacesTo<GridInputTest>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SpawnTest>().AsSingle().NonLazy();
            Container.DeclareSignal<GridInputMouseSignal>();
            Container.DeclareSignal<TetraminoLetterSignal>();
        }
    }
}
using Tetris.Model;
using Tetris.Signals;
using Zenject;

namespace Tetris.Installers
{
    public class TetraminoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TetraminoController>().AsSingle();
            Container.BindSignal<TetraminoShiftSignal>()
                .ToMethod<TetraminoController>((x, s) => x.Shift(s.Shift))
                .FromResolve();
            Container.BindSignal<TetraminoRotationSignal>()
                .ToMethod<TetraminoController>((x, s) => x.Rotate(s.Rotation))
                .FromResolve();

            Container.Bind<TetraminoFactory>().AsSingle();
            Container.Bind<FilledLinesCleaner>().FromNew().AsSingle().NonLazy();
        }
    }
}
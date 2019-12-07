using Zenject;

namespace Tetris.Model
{
    public class GameMaster : IInitializable
    {
        private readonly TetraminoController tetraminoController;
        private readonly TetraminoFactory tetraminoFactory;
        private readonly FilledLinesCleaner filledLinesCleaner;
        private readonly Spawner spawner;

        public GameMaster(TetraminoController tetraminoController,
            TetraminoFactory tetraminoFactory,
            FilledLinesCleaner filledLinesCleaner,
            Spawner spawner)
        {
            this.tetraminoController = tetraminoController;
            this.tetraminoFactory = tetraminoFactory;
            this.filledLinesCleaner = filledLinesCleaner;
            this.spawner = spawner;
        }

        public void Initialize()
        {
            // order here is important
            tetraminoController.OnTetraminoReleased += (lines) =>
            {
                filledLinesCleaner.ClearLinesIfFilled(lines);
                SpawnRoutine();
            };

            SpawnRoutine();
        }

        public void SpawnRoutine()
        {
            var newTetraminoType = spawner.Spawn();
            tetraminoController.Spawn(tetraminoFactory.Create(newTetraminoType));
        }
    }
}

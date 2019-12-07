using Tetris.Model;
using UnityEngine;
using Zenject;

namespace Tetris.Model
{
    public class Faller : MonoBehaviour
    {
        private TetraminoController linesCleaner;
        public float fallRate = 1f;
        private float tickTimer;

        [Inject]
        public void Construct(TetraminoController tetrisGrid)
        {
            this.linesCleaner = tetrisGrid;
        }

        private void Update()
        {
            tickTimer += Time.deltaTime;
            if (tickTimer >= fallRate)
            {
                tickTimer -= fallRate;
                linesCleaner.ApplyGravity();
            }
        }
    } 
}
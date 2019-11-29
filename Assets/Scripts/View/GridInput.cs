using Tetris.Signals;
using UnityEngine;
using Zenject;

namespace Tetris.View
{
    public class GridInput : MonoBehaviour
    {
        private Grid grid;
        private SignalBus signalBus;

        [Inject]
        public void Construct(Grid grid, SignalBus signalBus)
        {
            this.grid = grid;
            this.signalBus = signalBus;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
                Vector3Int mouseGridPosition = grid.WorldToCell(mouseWorldPosition);
                Debug.Log($"Mouse Screen Position:  {mouseScreenPosition}\n Mouse World Position: {mouseWorldPosition}\n Mouse Grid Position: {mouseGridPosition}");
                signalBus.Fire(new GridInputMouseSignal(new Vector2Int(mouseGridPosition.x, mouseGridPosition.y)));
            }
        }
    } 
}

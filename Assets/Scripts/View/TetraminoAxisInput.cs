using Tetris.Signals;
using UnityEngine;
using Zenject;

namespace Tetris.View
{
    public class TetraminoAxisInput : MonoBehaviour
    {
        private SignalBus signalBus;
        private bool horizontalInputPreviousFrame;
        //private bool verticalInputPreviousFrame;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        private void Update()
        {
            float xAxis = Input.GetAxis("Horizontal");
            float yAxis = Input.GetAxis("Vertical");

            if (xAxis != 0 && !horizontalInputPreviousFrame)
            {
                horizontalInputPreviousFrame = true;
                int x = xAxis > 0 ? 1 : -1;
                HandleInput(new Vector2Int(x, 0));
            }
            if (yAxis < 0)
            {
                HandleInput(new Vector2Int(0, -1));
            }

            horizontalInputPreviousFrame = xAxis != 0;
        }

        private void HandleInput(Vector2Int input)
        {
            signalBus.Fire(new TetraminoShiftSignal(input));
        }
    }
}
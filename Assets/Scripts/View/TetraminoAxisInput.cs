using Tetris.Model.Enumerators;
using Tetris.Signals;
using UnityEngine;
using Zenject;

namespace Tetris.View
{
    public class TetraminoAxisInput : MonoBehaviour
    {
        private SignalBus signalBus;
        private bool horizontalInputPreviousFrame;
        private bool verticalInputPreviousFrame;

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
                InputShift(new Vector2Int(x, 0));
            }
            if (yAxis != 0 && !verticalInputPreviousFrame)
            {
                verticalInputPreviousFrame = true;
                RotationDirectionEnum rotation = yAxis > 0
                    ? RotationDirectionEnum.Clockwise
                    : RotationDirectionEnum.Counterclockwise;
                InputRotation(rotation);
            }

            horizontalInputPreviousFrame = xAxis != 0;
            verticalInputPreviousFrame = yAxis != 0;
        }

        private void InputShift(Vector2Int input)
        {
            signalBus.Fire(new TetraminoShiftSignal(input));
        }

        private void InputRotation(RotationDirectionEnum rotation)
        {
            signalBus.Fire(new TetraminoRotationSignal(rotation));
        }
    }
}
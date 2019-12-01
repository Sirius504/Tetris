using System;
using Tetris.Model.Enumerators;
using Tetris.Signals;
using UnityEngine;
using Zenject;

namespace Tetris.View
{
    public class TetraminoStringInput : MonoBehaviour
    {
        private SignalBus signalBus;
        private bool inputPreviousFrame;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        private void Update()
        {
            if (Input.inputString != string.Empty && !inputPreviousFrame)
            {
                inputPreviousFrame = true;                
                HandleInput(Input.inputString);
            }
            else
            {
                inputPreviousFrame = false;
            }
        }

        private void HandleInput(string inputString)
        {
            if (Enum.TryParse(inputString.ToUpper(), out TetraminoTypeEnum result))            
                signalBus.Fire(new TetraminoLetterSignal(result));
        }
    }
}
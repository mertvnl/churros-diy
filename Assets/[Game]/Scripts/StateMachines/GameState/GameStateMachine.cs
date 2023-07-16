using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class GameStateMachine : MonoBehaviour
    {
        public List<GameStateBase> GameStates { get; private set; } = new List<GameStateBase>();
        public GameStateBase CurrentState { get; private set; }

        private int _currentStateIndex;

        private void Awake()
        {
            GameStateManager.Instance.SetCurrentStateMachine(this);
            SetGameStates();
            SetState(_currentStateIndex);
        }

        public void TriggerNextState() 
        {
            _currentStateIndex++;

            if (_currentStateIndex >= GameStates.Count)
                return;

            SetState(_currentStateIndex);
        }

        private void SetState(int stateIndex) 
        {
            CurrentState = GameStates[stateIndex];
            CurrentState.EnterState();
        }

        private void SetGameStates() 
        {
            GameStates = new List<GameStateBase>()
            {
                new BeginingState(this),
                new ChurrosDrawingState(this),
                new ChurrosFryingState(this),
                new WhippedCreamState(this),
                new SyrupState(this),
                new ToppingState(this),
            };
        }
    }
}


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
        }

        private void Start()
        {
            SetState(_currentStateIndex);
        }

        public void EnterNextState() 
        {
            _currentStateIndex++;

            if (_currentStateIndex >= GameStates.Count) 
            {
                GameStateManager.Instance.OnAllStatesCompleted.Invoke();
                return;
            }            

            SetState(_currentStateIndex);
        }

        private void SetState(int stateIndex) 
        {
            if (_currentStateIndex >= GameStates.Count)
                return;

            CurrentState?.ExitState();
            CurrentState = GameStates[stateIndex];
            CurrentState.EnterState();
        }

        private void SetGameStates() 
        {
            GameStates = new List<GameStateBase>()
            {
                new BeginingState(this),
                new ChurrosOrderState(this),
                new ChurrosDrawingState(this),
                new ChurrosFryingState(this),
                new WhippedCreamState(this),
                new SyrupState(this),
                new ToppingState(this),
            };
        }
    }
}


using Game.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Managers 
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        public GameStateMachine CurrentStateMachine { get; private set; }

        public UnityEvent OnEnterBeginningState { get; private set; } = new();
        public UnityEvent OnEnterCustomerOrderState { get; private set; } = new();
        public UnityEvent OnEnterChurrosDrawingState { get; private set; } = new();
        public UnityEvent OnExitChurrosDrawingState { get; private set; } = new();
        public UnityEvent OnEnterChurrosFryingState { get; private set; } = new();
        public UnityEvent OnEnterWhippedCreamState { get; private set; } = new();
        public UnityEvent OnExitWhippedCreamState { get; private set; } = new();
        public UnityEvent OnEnterSyrupState { get; private set; } = new();
        public UnityEvent OnEnterToppingState{ get; private set; } = new();
        public UnityEvent OnExitToppingState { get; private set; } = new();
        public UnityEvent OnAllStatesCompleted { get; private set; } = new();

        public void SetCurrentStateMachine(GameStateMachine stateMachine) 
        {
            CurrentStateMachine = stateMachine;
        }
    }
}


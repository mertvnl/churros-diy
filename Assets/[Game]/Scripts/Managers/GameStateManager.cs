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

        public UnityEvent OnEnterBeginningState { get; private set; } = new UnityEvent();
        public UnityEvent OnEnterChurrosDrawingState { get; private set; } = new UnityEvent();
        public UnityEvent OnEnterChurrosFryingState { get; private set; } = new UnityEvent();
        public UnityEvent OnEnterWhippedCreamState { get; private set; } = new UnityEvent();
        public UnityEvent OnEnterSyrupState { get; private set; } = new UnityEvent();
        public UnityEvent OnEnterToppingState{ get; private set; } = new UnityEvent();       

        public void SetCurrentStateMachine(GameStateMachine stateMachine) 
        {
            CurrentStateMachine = stateMachine;
        }
    }
}


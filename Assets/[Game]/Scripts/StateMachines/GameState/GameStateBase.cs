using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    [System.Serializable]
    public abstract class GameStateBase
    {
        public GameStateMachine StateMachine { get; private set; }
        public bool IsActive => StateMachine.CurrentState == this;

        public GameStateBase(GameStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract IEnumerator EnterState();
        public virtual IEnumerator ExitState() { yield break; }
    }
}


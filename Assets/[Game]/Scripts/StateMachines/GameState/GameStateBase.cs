using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace Game.Runtime 
{
    [System.Serializable]
    public abstract class GameStateBase
    {
        public GameStateMachine StateMachine { get; private set; }        

        public GameStateBase(GameStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void EnterState();
        public virtual void ExitState() { }
    }
}


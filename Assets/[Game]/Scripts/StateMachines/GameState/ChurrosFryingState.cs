using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class ChurrosFryingState : GameStateBase
    {
        public ChurrosFryingState(GameStateMachine stateMachine) : base(stateMachine) { }        

        public override void EnterState()
        {
            GameStateManager.Instance.OnEnterChurrosFryingState.Invoke();
        }
    }
}


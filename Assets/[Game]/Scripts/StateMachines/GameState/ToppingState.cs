using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{   
    public class ToppingState : GameStateBase
    {
        public ToppingState(GameStateMachine stateMachine) : base(stateMachine) { }       

        public override void EnterState()
        {
            GameStateManager.Instance.OnEnterToppingState.Invoke();
        }
    }
}


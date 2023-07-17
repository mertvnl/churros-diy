using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class ChurrosOrderState : GameStateBase
    {
        public ChurrosOrderState(GameStateMachine stateMachine) : base(stateMachine) { }        

        public override void EnterState()
        {
            GameStateManager.Instance.OnEnterCustomerOrderState.Invoke();
        }
    }
}

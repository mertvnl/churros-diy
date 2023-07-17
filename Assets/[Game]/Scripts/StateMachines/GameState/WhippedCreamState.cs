using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{   
    public class WhippedCreamState : GameStateBase
    {
        public WhippedCreamState(GameStateMachine stateMachine) : base(stateMachine) { }        

        public override void EnterState()
        {
            GameStateManager.Instance.OnEnterWhippedCreamState.Invoke();
        }
    }
}

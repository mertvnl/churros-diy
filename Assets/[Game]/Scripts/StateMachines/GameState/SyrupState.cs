using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{   
    public class SyrupState : GameStateBase
    {
        public SyrupState(GameStateMachine stateMachine) : base(stateMachine) { }        

        public override IEnumerator EnterState()
        {
            GameStateManager.Instance.OnEnterSyrupState.Invoke();
            yield break;
        }
    }
}

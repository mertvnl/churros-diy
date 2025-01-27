using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    [System.Serializable]
    public class BeginingState : GameStateBase
    {
        public BeginingState(GameStateMachine stateMachine) : base(stateMachine) { }        

        public override IEnumerator EnterState()
        {
            GameStateManager.Instance.OnEnterBeginningState.Invoke();
            yield break;
        }
    }
}

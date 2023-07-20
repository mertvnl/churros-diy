using Game.Enums;
using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{   
    public class WhippedCreamState : GameStateBase
    {
        public WhippedCreamState(GameStateMachine stateMachine) : base(stateMachine) { }

        private const float CAMERA_BLEND_DURATION = 0.75f;
        private const float STATE_DELAY = 0.35f;

        public override IEnumerator EnterState()
        {
            CameraManager.Instance.ActivateCamera(CameraID.ToppingCamera, CAMERA_BLEND_DURATION);
            yield return new WaitForSeconds(STATE_DELAY);
            GameStateManager.Instance.OnEnterWhippedCreamState.Invoke();
            yield break;
        }

        public override IEnumerator ExitState()
        {
            GameStateManager.Instance.OnExitWhippedCreamState.Invoke();
            yield break;
        }
    }
}

using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enums;

namespace Game.Runtime 
{
    public class ChurrosDeliveryState : GameStateBase
    {
        private const float STATE_DELAY = 0.5f;
        private const float CAMERA_BLEND_DURATION = 0.5f;

        public ChurrosDeliveryState(GameStateMachine stateMachine) : base(stateMachine) { }        

        public override IEnumerator EnterState()
        {
            yield return new WaitForSeconds(STATE_DELAY);
            CameraManager.Instance.ActivateCamera(CameraID.DeliveryCamera, CAMERA_BLEND_DURATION);
            GameStateManager.Instance.OnEnterChurrosDeliveryState.Invoke();
            yield break;
        }
    }
}


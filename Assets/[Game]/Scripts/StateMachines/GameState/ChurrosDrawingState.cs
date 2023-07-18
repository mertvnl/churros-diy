using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enums;

namespace Game.Runtime 
{   
    public class ChurrosDrawingState : GameStateBase
    {
        public ChurrosDrawingState(GameStateMachine stateMachine) : base(stateMachine) { }

        private const float CAMERA_BLEND_DURATION = 1f;

        public override void EnterState()
        {
            CameraManager.Instance.ActivateCamera(CameraID.DrawingCamera, CAMERA_BLEND_DURATION);
            GameStateManager.Instance.OnEnterChurrosDrawingState.Invoke();
        }

        public override void ExitState()
        {
            GameStateManager.Instance.OnExitChurrosDrawingState.Invoke();
        }
    }
}


using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enums;
using DG.Tweening;

namespace Game.Runtime 
{   
    public class ChurrosDrawingState : GameStateBase
    {
        public ChurrosDrawingState(GameStateMachine stateMachine) : base(stateMachine) { }

        private const float CAMERA_BLEND_DURATION = 0.75f;
        private const float STATE_DELAY = 0.25f;

        public override IEnumerator EnterState()
        {            
            CameraManager.Instance.ActivateCamera(CameraID.DrawingCamera, CAMERA_BLEND_DURATION);           
            yield return new WaitForSeconds(STATE_DELAY);
            UIManager.Instance.ShowPanel(PanelID.StateProgressIndicatorPanel);
            UIManager.Instance.ShowPanel(PanelID.DragToMovePanel);
            GameStateManager.Instance.OnEnterChurrosDrawingState.Invoke();
        }

        public override IEnumerator ExitState()
        {
            UIManager.Instance.HidePanel(PanelID.StateProgressIndicatorPanel);
            GameStateManager.Instance.OnExitChurrosDrawingState.Invoke();
            yield break;
        }
    }
}


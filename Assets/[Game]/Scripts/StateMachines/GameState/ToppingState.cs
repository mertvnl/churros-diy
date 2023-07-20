using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enums;

namespace Game.Runtime
{   
    public class ToppingState : GameStateBase
    {
        public ToppingState(GameStateMachine stateMachine) : base(stateMachine) { }

        private const float STATE_DELAY = 0.5f;

        public override IEnumerator EnterState()
        {
            yield return new WaitForSeconds(STATE_DELAY);
            UIManager.Instance.ShowPanel(PanelID.ToppingSelectionPanel);
            UIManager.Instance.ShowPanel(PanelID.StateProgressIndicatorPanel);
            UIManager.Instance.ShowPanel(PanelID.DragToMovePanel);
            GameStateManager.Instance.OnEnterToppingState.Invoke();
            yield break;
        }

        public override IEnumerator ExitState()
        {
            UIManager.Instance.HidePanel(PanelID.ToppingSelectionPanel);
            UIManager.Instance.HidePanel(PanelID.StateProgressIndicatorPanel);
            GameStateManager.Instance.OnExitToppingState.Invoke();
            yield break;
        }
    }
}


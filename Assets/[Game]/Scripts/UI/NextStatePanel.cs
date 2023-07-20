using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI 
{
    public class NextStatePanel : EasyPanel
    {
        public bool IsActive { get; private set; }

        protected override void OnEnable()
        {
            base.OnEnable();
            GameStateManager.Instance.OnStateChanged.AddListener(_ => HidePanel());
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GameStateManager.Instance.OnStateChanged.RemoveListener(_ => HidePanel());
        }

        public override void ShowPanel()
        {
            base.ShowPanel();
            IsActive = true;
        }

        public void NextStateButton() 
        {
            if (!IsActive)
                return;

            IsActive = false;
            GameStateManager.Instance.CurrentStateMachine.EnterNextState();
            HidePanel();
        }
    }
}


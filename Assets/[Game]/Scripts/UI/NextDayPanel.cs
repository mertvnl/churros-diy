using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI 
{
    public class NextDayPanel : EasyPanel
    {
        public bool IsActive { get; private set; } = true;

        protected override void OnEnable()
        {
            base.OnEnable();
            CurrencyManager.Instance.OnSuccessRewardClaimed.AddListener(ShowPanelAnimated);
            LevelManager.Instance.OnLevelFinished.AddListener(ResetValues);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            CurrencyManager.Instance.OnSuccessRewardClaimed.RemoveListener(ShowPanelAnimated);
            LevelManager.Instance.OnLevelFinished.RemoveListener(ResetValues);
        }

        public void NextDayButton() 
        {
            if (!IsActive)
                return;

            IsActive = false;
            HidePanel();
            GameManager.Instance.CompleteLevel(true);
            LevelManager.Instance.LoadNextLevel();
        }

        private void ResetValues() 
        {
            IsActive = true;
        }
    }
}

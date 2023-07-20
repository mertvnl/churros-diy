using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Interfaces;
using Game.Managers;

namespace Game.UI 
{
    public class StateProgressPanel : EasyPanel, IStateProgressIndicator
    {
        [Header("State Progress Panel")]
        [SerializeField] private Image fillImage;

        private void Awake()
        {
            ProgressManager.Instance.SetProgressIndicator(this);
        }

        public override void ShowPanel()
        {
            base.ShowPanel();
            UpdateProgress(0);
        }

        public override void ShowPanelAnimated()
        {
            base.ShowPanelAnimated();
            UpdateProgress(0);
        }

        public void UpdateProgress(float progress)
        {
            fillImage.fillAmount = progress;
        }
    }
}

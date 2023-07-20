using Game.Enums;
using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class FryerProgress : MonoBehaviour
    {
        public bool IsActive { get; private set; }
        public bool IsCompleted { get; private set; }

        private const float COMPLETE_THRESHOLD = 0.25f;

        private void OnEnable()
        {
            GameStateManager.Instance.OnEnterChurrosFryingState.AddListener(Activate);
            GameStateManager.Instance.OnExitChurrosFryingState.AddListener(Deactivate);
        }

        private void OnDisable()
        {
            GameStateManager.Instance.OnEnterChurrosFryingState.RemoveListener(Activate);
            GameStateManager.Instance.OnExitChurrosFryingState.RemoveListener(Deactivate);
        }

        private void Update()
        {            
            CheckComplete();
        }

        private void Activate()
        {
            IsActive = true;
        }

        private void Deactivate()
        {
            IsActive = false;
        }

        private void CheckComplete()
        {
            if (!IsActive || IsCompleted)
                return;

            if (FryingSlider.Instance.Slider.value >= COMPLETE_THRESHOLD)
            {
                IsCompleted = true;
                UIManager.Instance.ShowPanel(PanelID.NextStatePanel);
            }
        }
    }
}


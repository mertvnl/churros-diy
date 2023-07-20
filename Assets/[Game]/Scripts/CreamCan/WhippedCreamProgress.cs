using Dreamteck.Splines;
using Game.Enums;
using Game.Helpers;
using Game.Interfaces;
using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class WhippedCreamProgress : MonoBehaviour
    {
        public bool IsActive { get; private set; }
        public bool IsCompleted { get; private set; }
        private IStateProgressIndicator StateProgress => ProgressManager.Instance.CurrentStateIndicator;
        private int CurrentPointCount => creamGenerator.TotalPointCount;
        private int MaxSpawnCount => Mathf.FloorToInt(CreamGenerator.MAX_SPAWN_SPLINE_POINT * MAX_MULTIPLIER);

        [SerializeField] private CreamGenerator creamGenerator;

        private const float MAX_MULTIPLIER = 0.5f;

        private void OnEnable()
        {
            GameStateManager.Instance.OnEnterWhippedCreamState.AddListener(Activate);
            GameStateManager.Instance.OnExitWhippedCreamState.AddListener(Deactivate);
        }

        private void OnDisable()
        {
            GameStateManager.Instance.OnEnterWhippedCreamState.RemoveListener(Activate);
            GameStateManager.Instance.OnExitWhippedCreamState.RemoveListener(Deactivate);
        }

        private void Update()
        {
            UpdateProgress();
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

        private void UpdateProgress()
        {
            if (!IsActive || IsCompleted)
                return;

            float progress = GetProgress();
            StateProgress.UpdateProgress(progress);
        }

        private float GetProgress()
        {
            float progress = Utilities.Remap(CurrentPointCount, 0f, MaxSpawnCount, 0f, 1f);
            return progress;
        }

        private void CheckComplete()
        {
            if (IsCompleted)
                return;

            if (CurrentPointCount >= MaxSpawnCount)
            {
                IsCompleted = true;
                UIManager.Instance.ShowPanel(PanelID.NextStatePanel);
            }
        }
    }
}


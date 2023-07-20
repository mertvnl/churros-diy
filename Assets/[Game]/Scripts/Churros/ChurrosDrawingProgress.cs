using Dreamteck.Splines;
using Game.Interfaces;
using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Helpers;
using Game.Enums;

namespace Game.Runtime 
{
    public class ChurrosDrawingProgress : MonoBehaviour
    {
        private SplineComputer _splineComputer;
        public SplineComputer SplineComputer => _splineComputer == null ? _splineComputer = ChurrosManager.Instance.CurrentChurros.SplineComputer : _splineComputer;
        public bool IsActive { get; private set; }
        public bool IsCompleted { get; private set; }
        private IStateProgressIndicator StateProgress => ProgressManager.Instance.CurrentStateIndicator;
        private int CurrentPointCount => SplineComputer.pointCount;
        private int MaxSpawnCount => Mathf.FloorToInt(ChurrosGenerator.MAX_SPAWN_SPLINE_POINT * MAX_MULTIPLIER);

        private const float MAX_MULTIPLIER = 0.75f;

        private void OnEnable()
        {
            GameStateManager.Instance.OnEnterChurrosDrawingState.AddListener(Activate);
            GameStateManager.Instance.OnExitChurrosDrawingState.AddListener(Deactivate);
        }

        private void OnDisable()
        {
            GameStateManager.Instance.OnEnterChurrosDrawingState.RemoveListener(Activate);
            GameStateManager.Instance.OnExitChurrosDrawingState.RemoveListener(Deactivate);
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

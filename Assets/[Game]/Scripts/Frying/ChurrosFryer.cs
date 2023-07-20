using Game.Helpers;
using Game.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime
{
    public class ChurrosFryer : MonoBehaviour
    {
        public bool IsActive { get; private set; }
        public bool IsFrying { get; private set; }

        public UnityEvent OnStartFry { get; private set; } = new();
        public UnityEvent OnStopFry { get; private set; } = new();

        private void OnEnable()
        {
            GameStateManager.Instance.OnEnterChurrosFryingState.AddListener(Activate);
            GameStateManager.Instance.OnExitChurrosFryingState.AddListener(Deactivate);
            LeanInputController.Instance.OnFingerDown.AddListener(OnFingerDown);
            LeanInputController.Instance.OnFingerUp.AddListener(OnFingerUp);
        }

        private void OnDisable()
        {
            GameStateManager.Instance.OnEnterChurrosFryingState.RemoveListener(Activate);
            GameStateManager.Instance.OnExitChurrosFryingState.RemoveListener(Deactivate);
            LeanInputController.Instance.OnFingerDown.RemoveListener(OnFingerDown);
            LeanInputController.Instance.OnFingerUp.RemoveListener(OnFingerUp);

        }

        private void OnFingerDown() 
        {
            if (!IsActive)
                return;

            StartFrying();
        }

        private void OnFingerUp() 
        {
            StopFrying();
        }     

        private void StartFrying()
        {
            if (IsFrying)
                return;

            IsFrying = true;
            OnStartFry.Invoke();
        }

        private void StopFrying()
        {
            if (!IsFrying)
                return;

            IsFrying = false;
            OnStopFry.Invoke();
        }

        private void Activate() 
        {
            IsActive = true;
        }

        private void Deactivate() 
        {
            IsActive = false;
            StopFrying();
        }
    }
}

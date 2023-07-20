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
            InputManager.Instance.OnTouch.AddListener(FryChurro);
        }

        private void OnDisable()
        {
            GameStateManager.Instance.OnEnterChurrosFryingState.RemoveListener(Activate);
            GameStateManager.Instance.OnExitChurrosFryingState.RemoveListener(Deactivate);
            InputManager.Instance.OnTouch.RemoveListener(FryChurro);
        }

        private void FryChurro(TouchData data)
        {
            if (data.IsTouched && IsActive)
                StartFrying();
            else
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

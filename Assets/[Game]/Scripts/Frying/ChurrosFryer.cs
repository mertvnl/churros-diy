using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime
{
    public class ChurrosFryer : MonoBehaviour
    {
        public UnityEvent OnStartFry { get; private set; } = new();
        public UnityEvent OnStopFry { get; private set; } = new();

        private void OnEnable()
        {
            InputManager.Instance.OnTouch.AddListener(FryChurro);
        }

        private void OnDisable()
        {
            InputManager.Instance.OnTouch.RemoveListener(FryChurro);
        }

        private void FryChurro(TouchData data)
        {
            if (data.IsTouched)
                StartFrying();
            else
                StopFrying();
        }

        private void StartFrying()
        {
            Debug.Log("Fry");
            OnStartFry.Invoke();
        }

        private void StopFrying()
        {
            Debug.Log("Stop Fry");
            OnStopFry.Invoke();
        }
    }
}

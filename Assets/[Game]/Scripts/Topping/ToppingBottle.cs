using DG.Tweening;
using Game.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime 
{
    public class ToppingBottle : MonoBehaviour
    {
        public Vector3 DefaultPosition { get; private set; }
        public Vector3 OffsetPosition { get; private set; }
        public bool IsActive { get; private set; }
        public UnityEvent OnActivated { get; private set; } = new();
        public UnityEvent OnDisabled { get; private set; } = new();

        private const float MOVEMENT_OFFSET = 3f;
        private const float MOVEMENT_DURATION = 0.5f;       
        
        private Tween _movementTween;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            GameStateManager.Instance.OnEnterToppingState.AddListener(ActivateTopping);
            GameStateManager.Instance.OnExitToppingState.AddListener(DisableTopping);
        }

        private void OnDisable()
        {
            GameStateManager.Instance.OnEnterToppingState.RemoveListener(ActivateTopping);
            GameStateManager.Instance.OnExitToppingState.RemoveListener(DisableTopping);
        }

        private void Initialize()
        {
            DefaultPosition = transform.localPosition;
            OffsetPosition = DefaultPosition + MOVEMENT_OFFSET * Vector3.up;
            transform.localPosition = OffsetPosition;
        }

        private void ActivateTopping()
        {
            MoveTween(DefaultPosition, () =>
            {
                IsActive = true;
                OnActivated.Invoke();
            });
        }

        private void DisableTopping() 
        {
            IsActive = false;
            MoveTween(OffsetPosition);
            OnDisabled.Invoke();
        }

        private void MoveTween(Vector3 endValue, Action onComplete = null) 
        {
            _movementTween?.Kill();
            _movementTween = transform.DOMove(endValue, MOVEMENT_DURATION).SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
        }
    }
}

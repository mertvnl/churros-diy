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

        [SerializeField] private Transform body;

        private const float MOVEMENT_OFFSET = 0.5f;
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
            DefaultPosition = body.localPosition;
            OffsetPosition = DefaultPosition + MOVEMENT_OFFSET * Vector3.up;
            body.localPosition = OffsetPosition;
            SetBody(false);
        }

        private void ActivateTopping()
        {
            SetBody(true);
            MoveTween(DefaultPosition, () =>
            {
                IsActive = true;
                OnActivated.Invoke();
            });
        }

        private void DisableTopping() 
        {
            IsActive = false;
            OnDisabled.Invoke();
            MoveTween(OffsetPosition, () => 
            {
                SetBody(false);
            });            
        }

        private void MoveTween(Vector3 endValue, Action onComplete = null) 
        {
            _movementTween?.Kill();
            _movementTween = body.DOLocalMove(endValue, MOVEMENT_DURATION).SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
        }

        private void SetBody(bool isEnabled) 
        {
            body.gameObject.SetActive(isEnabled);
        }
    }
}

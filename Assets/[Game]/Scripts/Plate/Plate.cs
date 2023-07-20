using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class Plate : MonoBehaviour
    {
        private const float MOVEMENT_OFFSET = 1f;
        private const float MOVEMENT_DURATION = 0.5f;

        private Vector3 _defaultPosition;
        private Vector3 _offsetPosition;
        private Tween _movementTween;

        private void Awake()
        {
            _defaultPosition = transform.position;
            _offsetPosition = _defaultPosition + Vector3.left * MOVEMENT_OFFSET;
            transform.position = _offsetPosition;
        }

        private void OnEnable()
        {
            LevelManager.Instance.OnLevelStarted.AddListener(MoveTowardsInitial);
        }

        private void OnDisable() 
        {
            LevelManager.Instance.OnLevelStarted.RemoveListener(MoveTowardsInitial);
        }

        private void MoveTowardsInitial() 
        {
            MoveTween(_defaultPosition);
        }

        private void MoveTween(Vector3 endValue, Action onComplete = null)
        {
            _movementTween?.Kill();
            _movementTween = transform.DOMove(endValue, MOVEMENT_DURATION).SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
        }
    }
}


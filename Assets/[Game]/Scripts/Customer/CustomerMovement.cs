using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Runtime 
{
    public class CustomerMovement : MonoBehaviour
    {
        private CustomerRotation _rotation;
        public CustomerRotation CustomerRotation => _rotation == null ? _rotation = GetComponent<CustomerRotation>() : _rotation;
              
        private const float MOVEMENT_DURATION = 1f;

        private Tween _movementTween;

        public void MoveTowards(Vector3 targetPosition, float duration = MOVEMENT_DURATION, Action onComplete = null) 
        {
            CustomerRotation.LookPosition(targetPosition);

            _movementTween?.Kill();
            _movementTween = transform.DOMove(targetPosition, duration).SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
        }       
    }
}


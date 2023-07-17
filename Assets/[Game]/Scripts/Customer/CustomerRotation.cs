using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Runtime 
{
    public class CustomerRotation : MonoBehaviour
    {
        private const float ROTATE_DURATION = 0.25f;

        private Tween _rotationTween;       

        public void LookPosition(Vector3 targetPosition, float duration = ROTATE_DURATION, Action onComplete = null) 
        {
            Vector3 position = targetPosition;
            position.y = transform.position.y;

            Vector3 direction = (position - transform.position).normalized;
            if (direction.magnitude < 0.01f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            RotateTween(targetRotation, duration, onComplete);
        }

        private void RotateTween(Quaternion targetQuaternion, float duration, Action onComplete = null) 
        {
            _rotationTween?.Kill();
            _rotationTween = transform.DORotateQuaternion(targetQuaternion, duration).SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
        }
    }
}


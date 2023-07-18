using DG.Tweening;
using Game.Managers;
using Lean.Touch;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class CreamCan : MonoBehaviour
    {
        private LeanSelectableByFinger _leanSelectable;
        private LeanSelectableByFinger LeanSelectable => _leanSelectable == null ? _leanSelectable = GetComponent<LeanSelectableByFinger>() : _leanSelectable;

        [SerializeField] private Transform graphics;
        [SerializeField] private LayerMask ignoreLayer;
        [SerializeField] private GameObject indicator;

        private Vector3 _initialRotation;
        private float _initialY;
        private Tween _rotationTween;
        private Tween _deselectTween;
        private Vector3 _initialScale;
        private float _initialX;
        private bool _isDispoed;

        private const float TARGET_ROTATION_Z = 140f;
        private const float OFFSET_Y = 0.02f;
        private const float STARTING_POS_X = 1f;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _initialRotation = graphics.localEulerAngles;
            _initialY = graphics.position.y;
            _initialScale = graphics.localScale;
            _initialX = graphics.localPosition.x;
            graphics.localScale = Vector3.zero;
            graphics.localPosition += Vector3.right * STARTING_POS_X;
        }

        private void OnEnable()
        {
            LeanSelectable.OnSelectedFinger.AddListener(SelectTween);
            LeanSelectable.OnSelectedFingerUp.AddListener(DeselectTween);
            GameStateManager.Instance.OnEnterWhippedCreamState.AddListener(InitialMovement);
            GameStateManager.Instance.OnExitWhippedCreamState.AddListener(Dispose);
        }

        private void OnDisable()
        {
            LeanSelectable.OnSelectedFinger.RemoveListener(SelectTween);
            LeanSelectable.OnSelectedFingerUp.RemoveListener(DeselectTween);
            GameStateManager.Instance.OnEnterWhippedCreamState.RemoveListener(InitialMovement);
            GameStateManager.Instance.OnExitWhippedCreamState.RemoveListener(Dispose);
        }

        private void Update()
        {
            UpdateHeight();
        }

        [Button]
        private void InitialMovement()
        {
            graphics.DOScale(_initialScale, 0.25f);
            graphics.DOLocalMoveX(_initialX, 1f).OnComplete(OnMovementCompleted).SetDelay(0.5f).SetDelay(0.5f);

            void OnMovementCompleted()
            {
                indicator.SetActive(true);
                LeanSelectable.enabled = true;
            }
        }

        private void UpdateHeight()
        {
            if (!LeanSelectable.IsSelected)
                return;

            graphics.position = Vector3.Lerp(graphics.position, GetHeight(), 7 * Time.deltaTime);
        }

        private Vector3 GetHeight()
        {
            if (Physics.Raycast(graphics.position + (Vector3.up * 0.1f), Vector3.down, out RaycastHit hit, 1000, ~ignoreLayer))
                return new(graphics.position.x, hit.point.y + OFFSET_Y, graphics.position.z);

            return Vector3.zero;
        }

        private void SelectTween(LeanFinger arg0)
        {
            if (_isDispoed)
                return;

            Vector3 targetRotation = new(_initialRotation.x, _initialRotation.y, TARGET_ROTATION_Z);
            _rotationTween?.Kill();
            _rotationTween = graphics.DOLocalRotate(targetRotation, 0.6f);
        }

        private void DeselectTween(LeanFinger arg0)
        {
            if (_isDispoed)
                return;

            _rotationTween?.Kill();
            _rotationTween = graphics.DOLocalRotate(_initialRotation, 0.25f);
            _deselectTween = graphics.DOMoveY(_initialY, 0.25f);
        }

        private void Dispose()
        {
            _isDispoed = true;
            indicator.SetActive(false);
            _deselectTween?.Kill();

            graphics.DOLocalMoveX(STARTING_POS_X, 1f).OnComplete(OnMovementCompleted);

            void OnMovementCompleted()
            {
                graphics.DOScale(Vector3.zero, 0.25f);
            }
        }
    }
}
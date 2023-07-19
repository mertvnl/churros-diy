using DG.Tweening;
using Game.Managers;
using Lean.Touch;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.Runtime
{
    public class PastryBag : MonoBehaviour
    {
        private ChurrosGenerator _churrosGenerator;
        private ChurrosGenerator ChurrosGenerator => _churrosGenerator == null ? _churrosGenerator = GetComponent<ChurrosGenerator>() : _churrosGenerator;

        private LeanDragTranslateAlong _leanMover;
        private LeanDragTranslateAlong LeanMover => _leanMover == null ? _leanMover = GetComponent<LeanDragTranslateAlong>() : _leanMover;

        public bool IsControlable { get; private set; }
        public UnityEvent OnInputStart { get; private set; } = new();
        public UnityEvent OnInputStop { get; private set; } = new();

        [SerializeField] private Transform graphics;
        [SerializeField] private GameObject indicator;

        private Vector3 _initialScale;
        private float _initialX;

        private const float STARTING_POS_X = 1f;

        private void OnEnable()
        {
            GameStateManager.Instance.OnEnterChurrosDrawingState.AddListener(InitialMovement);
            GameStateManager.Instance.OnExitChurrosDrawingState.AddListener(Dispose);
        }

        private void OnDisable()
        {
            GameStateManager.Instance.OnEnterChurrosDrawingState.RemoveListener(InitialMovement);
            GameStateManager.Instance.OnExitChurrosDrawingState.RemoveListener(Dispose);
        }

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (!IsControlable) return;

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                OnInputStart.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnInputStop.Invoke();
            }
        }

        private void Initialize()
        {
            IsControlable = false;
            LeanMover.enabled = false;
            ChurrosGenerator.SetActivation(false);
            _initialScale = graphics.localScale;
            _initialX = graphics.localPosition.x;
            graphics.localScale = Vector3.zero;
            graphics.localPosition += Vector3.left * STARTING_POS_X;
        }

        [Button]
        private void InitialMovement()
        {
            graphics.DOScale(_initialScale, 0.25f).SetDelay(0.5f);
            graphics.DOLocalMoveX(_initialX, 1f).OnComplete(OnMovementCompleted).SetDelay(0.5f);

            void OnMovementCompleted()
            {
                IsControlable = true;
                LeanMover.enabled = true;
                ChurrosGenerator.SetActivation(true);
                indicator.SetActive(true);
            }
        }

        private void Dispose()
        {
            IsControlable = false;
            LeanMover.enabled = false;
            graphics.DOLocalMoveX(-STARTING_POS_X, 1f).OnComplete(OnMovementCompleted);
            indicator.SetActive(false);

            void OnMovementCompleted()
            {
                graphics.DOScale(Vector3.zero, 0.25f);
            }
        }
    }
}
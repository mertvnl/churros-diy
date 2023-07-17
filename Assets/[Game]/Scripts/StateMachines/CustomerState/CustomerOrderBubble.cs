using Game.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game.Utilities;
using DG.Tweening;
using Game.Managers;

namespace Game.Runtime 
{
    public class CustomerOrderBubble : MonoBehaviour
    {
        [SerializeField] private OrderPhraseData orderPhraseData;
        [SerializeField] private GameObject orderBubble;
        [SerializeField] private TextMeshPro orderText;

        private float SCALE_MIN_SCALE = 0.01f;
        private float SCALE_DURATION = 0.25f;
        private Ease SCALE_EASE = Ease.OutBack;

        private Vector3 _defaultBubbleScale;
        private Tween _scaleTween;

        private void Awake()
        {
            _defaultBubbleScale = orderBubble.transform.localScale;
            DisableBubble();
        }

        private void OnEnable()
        {
            GameStateManager.Instance.OnEnterCustomerOrderState.AddListener(ActivateBubble);
            LevelManager.Instance.OnLevelStarted.AddListener(DisableBubble);
        }

        private void OnDisable()
        {
            GameStateManager.Instance.OnEnterCustomerOrderState.RemoveListener(ActivateBubble);
            LevelManager.Instance.OnLevelStarted.RemoveListener(DisableBubble);
        }

        private void ActivateBubble() 
        {
            UpdateOrderText();
            ScaleTween(_defaultBubbleScale * SCALE_MIN_SCALE, _defaultBubbleScale);
            orderBubble.SetActive(true);
        }

        private void DisableBubble() 
        {
            orderBubble.SetActive(false);
        }

        private void UpdateOrderText() 
        {
            string text = GetRandomPhrase();
            orderText.SetText(text);
        }

        private void ScaleTween(Vector3 startValue, Vector3 endValue) 
        {
            orderBubble.transform.localScale = startValue;

            _scaleTween?.Kill();
            _scaleTween = orderBubble.transform.DOScale(endValue, SCALE_DURATION).SetEase(SCALE_EASE);
        }

        private string GetRandomPhrase() 
        {
            List<string> phrases = new(orderPhraseData.OrderPhrases);
            phrases.Shuffle();
            return phrases[0];
        }
    }
}


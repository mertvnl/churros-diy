using Game.Managers;
using Game.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class ToppingBottleVisual : MonoBehaviour
    {
        [SerializeField] private MeshRenderer bottleRenderer;

        private const int TIP_INDEX = 0;
        private const int BODY_INDEX = 1;

        private MaterialPropertyBlock _tipMaterialBlock;
        private MaterialPropertyBlock _bodyMaterialBlock;

        private void Awake()
        {
            _tipMaterialBlock = new();
            _bodyMaterialBlock = new();
        }

        private void OnEnable()
        {
            ToppingManager.Instance.OnToppingChanged.AddListener(UpdateVisual);
        }

        private void OnDisable()
        {
            ToppingManager.Instance.OnToppingChanged.RemoveListener(UpdateVisual);
        }

        private void Start()
        {
            UpdateVisual(ToppingManager.Instance.CurrentToppingData);
        }

        private void UpdateVisual(ToppingData toppingData) 
        {
            SetTipColor(toppingData.BottleTipColor);
            SetBodyTexture(toppingData.BottleBodyTexture);
        }       

        private void SetTipColor(Color color) 
        {
            _tipMaterialBlock.SetColor("_Color", color);
            bottleRenderer.SetPropertyBlock(_tipMaterialBlock, TIP_INDEX);
        }

        private void SetBodyTexture(Texture texture) 
        {
            _bodyMaterialBlock.SetTexture("_MainTex", texture);
            bottleRenderer.SetPropertyBlock(_bodyMaterialBlock, BODY_INDEX);
        }
    }
}


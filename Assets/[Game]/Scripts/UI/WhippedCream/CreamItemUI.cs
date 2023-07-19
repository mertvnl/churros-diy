using Game.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class CreamItemUI : MonoBehaviour
    {
        private Toggle _toggle;
        private Toggle Toggle => _toggle == null ? _toggle = GetComponent<Toggle>() : _toggle;

        [SerializeField] private GameObject outline;
        [SerializeField] private Image creamCanImage;

        private CreamData _data;

        private void OnEnable()
        {
            Toggle.onValueChanged.AddListener(ToggleItem);
        }

        private void OnDisable()
        {
            Toggle.onValueChanged.RemoveListener(ToggleItem);
        }

        public void Initialize(CreamData data, ToggleGroup toggleGroup)
        {
            _data = data;
            Toggle.group = toggleGroup;
            SetVisuals();
        }

        private void SetVisuals()
        {
            if (_data == null) 
                return;

            creamCanImage.sprite = _data.UISprite;
        }

        public void ToggleItem(bool status)
        {
            outline.SetActive(status);
            EventManager.OnCreamItemSelected.Invoke(_data);
        }
    }
}
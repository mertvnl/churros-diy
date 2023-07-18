using Game.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class CreamCanColorChanger : MonoBehaviour
    {
        [SerializeField] private Renderer renderer;

        private void OnEnable()
        {
            EventManager.OnCreamItemSelected.AddListener(UpdateColor);
        }

        private void OnDisable()
        {
            EventManager.OnCreamItemSelected.RemoveListener(UpdateColor);
        }

        private void UpdateColor(CreamData data)
        {
            renderer.material.color = data.Color;
        }
    }
}
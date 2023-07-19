using Game.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class CreamCanVisualChanger : MonoBehaviour
    {
        [SerializeField] private Renderer renderer;

        private const int COLOR_MATERIAL_INDEX = 0;
        private const int TEXTURE_MATERIAL_INDEX = 1;

        private void OnEnable()
        {
            EventManager.OnCreamItemSelected.AddListener(UpdateVisuals);
        }

        private void OnDisable()
        {
            EventManager.OnCreamItemSelected.RemoveListener(UpdateVisuals);
        }

        private void UpdateVisuals(CreamData data)
        {
            Material[] materials = renderer.materials;

            materials[COLOR_MATERIAL_INDEX].color = data.Color;
            materials[TEXTURE_MATERIAL_INDEX].mainTexture = data.Texture; 
            renderer.materials = materials;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI 
{
    public class ToppingToggle : MonoBehaviour
    {
        private Toggle _toggle;
        private Toggle Toggle => _toggle == null ? _toggle = GetComponent<Toggle>() : _toggle;

        public void Initialize(ToggleGroup toggleGroup) 
        {
            Toggle.group = toggleGroup;
        }


    }
}


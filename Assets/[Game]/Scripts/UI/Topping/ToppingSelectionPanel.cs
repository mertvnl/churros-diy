using Game.Managers;
using Game.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI 
{
    public class ToppingSelectionPanel : EasyPanel
    {
        private ToggleGroup _toggleGroup;
        private ToggleGroup ToggleGroup => _toggleGroup == null ? _toggleGroup = GetComponentInChildren<ToggleGroup>() : _toggleGroup;
        public ToppingDatabase ToppingDatabase => ToppingManager.Instance.ToppingDatabase;
        public List<ToppingToggle> ToppingToggles { get; private set; } = new();

        [Header("Topping Selection Panel")]
        [SerializeField] private GameObject togglePrefab;
        [SerializeField] private Transform content;       

        private void Awake()
        {
            Initialize();
        }

        public override void ShowPanel()
        {
            base.ShowPanel();
            SelectDefault();
        }

        public override void ShowPanelAnimated()
        {
            base.ShowPanelAnimated();
            SelectDefault();
        }

        private void Initialize()
        {
            for (int i = 0; i < ToppingDatabase.Toppings.Count; i++)
            {
                bool isDefault = i == 0;
                CreateToggle(ToppingDatabase.Toppings[i], isDefault);
            }
        }                

        private void CreateToggle(ToppingData toppingData, bool isDefault) 
        {
            ToppingToggle toppingToggle = Instantiate(togglePrefab, content).GetComponent<ToppingToggle>();           
            toppingToggle.Initialize(toppingData, ToggleGroup, isDefault);
        }

        private void SelectDefault() 
        {
            if (ToppingToggles.Count == 0)
                return;

            ToppingToggles[0].ToggleItem(true);
        }
    }
}
 
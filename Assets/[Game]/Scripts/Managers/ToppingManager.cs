using Game.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Game.Runtime;
using Game.Helpers;
using Game.Enums;

namespace Game.Managers 
{
    public class ToppingManager : Singleton<ToppingManager>
    {
        public List<Topping> SpawnedToppings { get; private set; } = new();
        public ToppingData CurrentToppingData { get; private set; }
        public bool IsNextStateButtonActive { get; private set; }
        public bool IsCompleted { get; private set; }
        public UnityEvent<ToppingData> OnToppingChanged { get; private set; } = new();

        [field: SerializeField] public ToppingDatabase ToppingDatabase { get; private set; }

        private const int MAX_TOPPING_COUNT = 150;
        private const int COMPLETE_THRESHOLD = 50;

        private void OnEnable()
        {
            LevelManager.Instance.OnLevelFinished.AddListener(ResetValues);
        }

        private void OnDisable()
        {
            LevelManager.Instance.OnLevelFinished.RemoveListener(ResetValues);
        }

        public void SetCurrentToppingData(ToppingData toppingData) 
        {
            CurrentToppingData = toppingData;
        }

        public void AddTopping(Topping topping) 
        {
            SpawnedToppings.Add(topping);
            UpdateProgressIndicator();
            CheckNextStateButton();
            CheckComplete();
        }

        private void UpdateProgressIndicator() 
        {
            float progress = Utilities.Remap(SpawnedToppings.Count, 0, COMPLETE_THRESHOLD, 0f, 1f);
            ProgressManager.Instance.CurrentStateIndicator.UpdateProgress(progress);
        }

        private void CheckNextStateButton() 
        {
            if (IsNextStateButtonActive)
                return;
            
            if (SpawnedToppings.Count >= COMPLETE_THRESHOLD)
            {
                IsNextStateButtonActive = true;
                UIManager.Instance.ShowPanel(PanelID.NextStatePanel);
            }
        }

        private void CheckComplete() 
        {
            if (IsCompleted)
                return;

            if (SpawnedToppings.Count >= MAX_TOPPING_COUNT)
            {
                IsCompleted = true;
                GameStateManager.Instance.CurrentStateMachine.EnterNextState();
            }
        }

        private void ResetValues() 
        {
            IsCompleted = false;
            IsNextStateButtonActive = false;
            SpawnedToppings.Clear();
        }
    }
}


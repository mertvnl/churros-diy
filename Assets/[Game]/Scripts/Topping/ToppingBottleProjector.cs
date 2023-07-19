using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class ToppingBottleProjector : MonoBehaviour
    {
        private ToppingBottle _toppingBottle;
        private ToppingBottle ToppingBottle => _toppingBottle == null ? _toppingBottle = GetComponentInParent<ToppingBottle>() : _toppingBottle;

        [SerializeField] private GameObject projector;

        private void Awake()
        {
            DeactivateProjector();
        }

        private void OnEnable()
        {
            ToppingBottle.OnActivated.AddListener(ActivateProjector);
            ToppingBottle.OnDisabled.AddListener(DeactivateProjector);
        }

        private void OnDisable()
        {
            ToppingBottle.OnActivated.RemoveListener(ActivateProjector);
            ToppingBottle.OnDisabled.RemoveListener(DeactivateProjector);
        }

        private void ActivateProjector() 
        {
            SetProjector(true);
        }

        private void DeactivateProjector() 
        {
            SetProjector(false);
        }

        private void SetProjector(bool isEnabled)
        {
            projector.SetActive(isEnabled);
        }
    }
}


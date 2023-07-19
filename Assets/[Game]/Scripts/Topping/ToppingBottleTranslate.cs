using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class ToppingBottleTranslate : MonoBehaviour
    {
        private ToppingBottle _toppingBottle;
        private ToppingBottle ToppingBottle => _toppingBottle == null ? _toppingBottle = GetComponentInParent<ToppingBottle>() : _toppingBottle;

        private LeanDragTranslateAlong _leanTranslate;
        private LeanDragTranslateAlong LeanTranslate => _leanTranslate == null ? _leanTranslate = GetComponent<LeanDragTranslateAlong>() : _leanTranslate;

        private void Awake()
        {
            DeactivateTranslate();
        }

        private void OnEnable()
        {
            ToppingBottle.OnActivated.AddListener(ActivateTranslate);
            ToppingBottle.OnDisabled.AddListener(DeactivateTranslate);
        }

        private void OnDisable()
        {
            ToppingBottle.OnActivated.RemoveListener(ActivateTranslate);
            ToppingBottle.OnDisabled.RemoveListener(DeactivateTranslate);
        }

        private void ActivateTranslate()
        {
            SetTranslate(true);
        }

        private void DeactivateTranslate()
        {
            SetTranslate(false);
        }

        private void SetTranslate(bool isEnabled)
        {
            LeanTranslate.enabled = isEnabled;
        }
    }
}

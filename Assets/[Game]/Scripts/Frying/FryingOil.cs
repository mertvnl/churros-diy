using Game.Interfaces;
using Game.Models;
using System;
using UnityEngine;

namespace Game.Props
{
    public class FryingOil : MonoBehaviour
    {
        public static Action<FryingData> OnFry;

        private IFryable _curFryable;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IFryable>(out _curFryable))
                _curFryable.StartFrying();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IFryable>(out _curFryable))
                _curFryable.StopFrying();
        }
    }
}


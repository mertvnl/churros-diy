using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime 
{
    public class Customer : MonoBehaviour
    {
        public Vector3 CashierPosition { get; private set; }
        public Vector3 ExitPosition { get; private set; } 
        public UnityEvent OnInitialized { get; private set; } = new();         

        public void Initialize(Vector3 cashierPosition, Vector3 exitPosition) 
        {
            CashierPosition = cashierPosition;
            ExitPosition = exitPosition;
            OnInitialized.Invoke();
        }
    }
}

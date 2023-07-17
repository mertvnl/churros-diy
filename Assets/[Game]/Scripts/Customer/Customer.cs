using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime 
{
    public class Customer : MonoBehaviour
    {
        public UnityEvent OnInitialized { get; private set; } = new(); 

        public void Initialize() 
        {
            OnInitialized.Invoke();
        }
    }
}

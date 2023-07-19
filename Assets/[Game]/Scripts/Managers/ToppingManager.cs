using Game.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Managers 
{
    public class ToppingManager : Singleton<ToppingManager>
    {     
        public ToppingData CurrentToppingData { get; private set; }
        public UnityEvent<ToppingData> OnToppingChanged { get; private set; } = new();

        [field: SerializeField] public ToppingDatabase ToppingDatabase { get; private set; }

        public void SetCurrentToppingData(ToppingData toppingData) 
        {
            CurrentToppingData = toppingData;
        }
    }
}


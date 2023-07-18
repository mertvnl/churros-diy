using Game.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Managers 
{
    public class ToppingManager : Singleton<ToppingManager>
    {        
        public ToppingData DefaultToppingData => ToppingDatabase.Toppings[0];
        public UnityEvent<ToppingData> OnToppingChanged { get; private set; } = new();

        [field: SerializeField] public ToppingDatabase ToppingDatabase { get; private set; }

    }
}


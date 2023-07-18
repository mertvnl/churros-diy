using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models 
{
    [CreateAssetMenu(fileName = "Topping Database", menuName = "Scriptable Objects/Create ToppingDatabase")]
    public class ToppingDatabase : ScriptableObject
    {
        public List<ToppingData> Toppings = new();
    }
}


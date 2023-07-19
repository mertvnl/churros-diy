using Game.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models 
{
    [CreateAssetMenu(fileName = "Topping Data", menuName = "Scriptable Objects/Create ToppingData")]
    public class ToppingData : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public List<PoolID> PoolIDs = new();
    }
}


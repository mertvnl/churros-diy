using Game.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interfaces;

namespace Game.Models 
{
    [CreateAssetMenu(fileName = "Topping Data", menuName = "Scriptable Objects/Create ToppingData")]
    public class ToppingData : ScriptableObject, IIngredientData
    {
        public string Name;
        public Sprite Icon;        
        [Space]
        public Color BottleTipColor;
        public Texture BottleBodyTexture;
        [Space]        
        public List<PoolID> PoolIDs = new();
        [field: SerializeField] public bool IsBad { get; private set; }
    }
}


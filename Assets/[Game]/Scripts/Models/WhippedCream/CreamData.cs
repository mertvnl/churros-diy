using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interfaces;

namespace Game.Models
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create New Cream Data")]
    public class CreamData : ScriptableObject, IIngredientData
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public Sprite UISprite { get; private set; }
        [field: SerializeField] public Texture2D Texture { get; private set; }
        [field: SerializeField] public bool IsBad { get; private set; }
    }
}
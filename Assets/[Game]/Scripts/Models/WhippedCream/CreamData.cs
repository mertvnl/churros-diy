using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create New Cream Data")]
    public class CreamData : ScriptableObject
    {
        [field: SerializeField] public Color Color { get; private set; }
    }
}
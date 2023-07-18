using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create Cream Data Holder")]
    public class CreamDataHolder : ScriptableObject
    {
        [field: SerializeField] public List<CreamData> Creams { get; private set; } = new();
        [field: SerializeField, ValueDropdown(nameof(Creams))] public CreamData DefaultCreamData { get; private set; }
    }
}
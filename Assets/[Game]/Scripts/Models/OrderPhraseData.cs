using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models 
{
    [CreateAssetMenu(fileName = "Order Phrase Data", menuName = "Scriptable Objects/Create OrderPhraseData")]
    public class OrderPhraseData : ScriptableObject
    {
        public List<string> OrderPhrases = new();
    }
}


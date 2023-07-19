using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models 
{
    [CreateAssetMenu(fileName = "Pool Database", menuName = "Scriptable Objects/Create Pool Database")]
    public class PoolDatabase : ScriptableObject
    {        
        public List<Pool> Pools = new List<Pool>();
    }
}


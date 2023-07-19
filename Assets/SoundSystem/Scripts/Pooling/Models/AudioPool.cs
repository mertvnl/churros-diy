using Game.GlobalVariables;
using Game.Managers;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Game.Models
{
    [System.Serializable]
    public class AudioPool
    {
        [ValueDropdown(nameof(GetPoolingIDs))]
        public int PoolID;
        public GameObject Prefab;
        public int InitialSize;

        private ValueDropdownList<int> GetPoolingIDs()
        {
            ValueDropdownList<int> poolIDs = new();
            FieldInfo[] fields = typeof(PoolingStrings).GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                poolIDs.Add(field.Name, int.Parse(field.GetValue(null).ToString()));
            }

            return poolIDs;
        }
    }
}

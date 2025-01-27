using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using Game.Enums;
using Game.Runtime;
using Game.Models;
using System.Linq;
using UnityEngine.Pool;

namespace Game.Managers 
{
    public class PoolingManager : Singleton<PoolingManager>
    {
        private Dictionary<PoolID, Stack<PoolObject>> _poolStacksByID = new Dictionary<PoolID, Stack<PoolObject>>();
        public Dictionary<PoolID, Stack<PoolObject>> PoolStacksByID { get => _poolStacksByID; private set => _poolStacksByID = value; }

        private Dictionary<PoolID, Pool> _poolsByID = new Dictionary<PoolID, Pool>();
        public Dictionary<PoolID, Pool> PoolsByID { get => _poolsByID; private set => _poolsByID = value; }

        [SerializeField] private PoolDatabase poolDatabase;

        private void Awake()
        {
            SetPoolCollection();
            SetInitialPoolStacks();
        }

        public PoolObject Instantiate(PoolID poolID, Vector3 position, Quaternion rotation)
        {
            if (!PoolStacksByID.ContainsKey(poolID))
            {
                Debug.LogError("Pool with ID " + poolID + " does not exist.");
                return null;
            }

            PoolObject poolObject = PopPoolObject(poolID);
            poolObject.transform.SetPositionAndRotation(position, rotation);
            poolObject.gameObject.SetActive(true);
            poolObject.Initialize();

            return poolObject;
        }

        public void DestroyPoolObject(PoolObject poolObject)
        {
            if (!PoolStacksByID.ContainsKey(poolObject.PoolID))
                return;

            poolObject.gameObject.SetActive(false);
            poolObject.transform.SetParent(transform);
            poolObject.Dispose();

            PoolStacksByID[poolObject.PoolID].Push(poolObject);
        }

        private PoolObject PopPoolObject(PoolID poolID)
        {
            int stackCount = PoolStacksByID[poolID].Count;
            for (int i = 0; i < stackCount; i++)
            {
                PoolObject poolObject = PoolStacksByID[poolID].Pop();
                if (poolObject != null)
                    return poolObject;
            }
            return CreatePoolObject(poolID);
        }

        private PoolObject CreatePoolObject(PoolID poolID)
        {
            if (!PoolsByID.ContainsKey(poolID))
                return null;

            PoolObject poolObject = Instantiate(PoolsByID[poolID].Prefab).GetComponent<PoolObject>();
            poolObject.transform.SetParent(transform);
            poolObject.gameObject.SetActive(false);

            return poolObject;
        }

        private void SetInitialPoolStacks()
        {
            foreach (Pool pool in poolDatabase.Pools)
            {
                if (PoolStacksByID.ContainsKey(pool.Prefab.PoolID))
                    continue;

                Stack<PoolObject> poolStack = new Stack<PoolObject>();
                for (int i = 0; i < pool.InitialSize; i++)
                {
                    PoolObject poolObject = CreatePoolObject(pool.Prefab.PoolID);
                    poolStack.Push(poolObject);
                }

                PoolStacksByID.Add(pool.Prefab.PoolID, poolStack);
            }
        }

        private void SetPoolCollection()
        {
            foreach (var pool in poolDatabase.Pools)
            {
                if (!PoolsByID.ContainsKey(pool.Prefab.PoolID))
                {
                    PoolsByID.Add(pool.Prefab.PoolID, pool);
                }
            }
        }        
    }
}

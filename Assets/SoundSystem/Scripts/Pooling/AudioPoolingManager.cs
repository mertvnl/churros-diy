using System.Collections.Generic;
using Game.Actors.PoolingSystem;
using UnityEngine;
using Game.Models;
namespace Game.Managers 
{
    public class AudioPoolingManager : Singleton<AudioPoolingManager>
    {
        private Dictionary<int, Stack<GameObject>> _poolStacksByID = new();
        public Dictionary<int, Stack<GameObject>> PoolStacksByID { get => _poolStacksByID; private set => _poolStacksByID = value; }

        private Dictionary<int, AudioPool> _poolsByID = new();
        public Dictionary<int, AudioPool> PoolsByID { get => _poolsByID; private set => _poolsByID = value; }

        [SerializeField] private List<AudioPool> _pools = new();

        private void Start()
        {
            SetPoolCollection();
            SetInitialPoolStacks();
        }

        public GameObject InstantiateFromPool(int poolID)
        {
            if (!PoolStacksByID.ContainsKey(poolID))
            {
                Debug.LogError("Pool with ID " + poolID + " does not exist.");
                return null;
            }

            GameObject poolObject;

            if (PoolStacksByID[poolID].Count <= 0)
            {
                poolObject = CreatePoolObject(poolID);
            }
            else
            {
                poolObject = PoolStacksByID[poolID].Pop();                
            }

            if (poolObject == null) 
            {
                poolObject = CreatePoolObject(poolID);
            }                

            if (poolObject != null)
            {
                poolObject.SetActive(true);
            }                

            return poolObject;
        }

        public GameObject InstantiateFromPool(int poolID, Vector3 position, Quaternion rotation)
        {
            GameObject poolObject = InstantiateFromPool(poolID);
            if (poolObject == null)
                return null;
            poolObject.transform.SetPositionAndRotation(position, rotation);
            return poolObject;
        }

        public GameObject InstantiateFromPool(int poolID, Transform parent)
        {
            GameObject poolObject = InstantiateFromPool(poolID);
            if (poolObject == null)
                return null;
            poolObject.transform.SetParent(parent);
            return poolObject;
        }

        public void DestroyPoolObject(GameObject obj)
        {
            if (!obj.TryGetComponent(out PoolObject poolObject))
                return;

            if (!PoolStacksByID.ContainsKey(poolObject.PoolID))
                return;            

            obj.SetActive(false);
            obj.transform.SetParent(transform);
            poolObject.Dispose();

            PoolStacksByID[poolObject.PoolID].Push(obj);
        }

        private void SetInitialPoolStacks()
        {
            foreach (AudioPool pool in _pools)
            {
                Stack<GameObject> poolStack = new();
                for (int i = 0; i < pool.InitialSize; i++)
                {
                    GameObject poolObject = CreatePoolObject(pool.PoolID);
                    poolStack.Push(poolObject);
                }

                if (!PoolStacksByID.ContainsKey(pool.PoolID))
                {
                    PoolStacksByID.Add(pool.PoolID, poolStack);
                }
            }
        }

        private void SetPoolCollection()
        {
            foreach (var pool in _pools)
            {
                if (!PoolsByID.ContainsKey(pool.PoolID))
                {
                    PoolsByID.Add(pool.PoolID, pool);
                }
            }
        }       

        private GameObject CreatePoolObject(int poolID)
        {
            if (!PoolsByID.ContainsKey(poolID))
                return null;

            GameObject poolObject = Instantiate(PoolsByID[poolID].Prefab);

            if (!poolObject.TryGetComponent(out PoolObject _))
            {
                poolObject.AddComponent<PoolObject>().Initialize(poolID);
            }

            poolObject.transform.SetParent(transform);
            poolObject.SetActive(false);
            return poolObject;
        }
    }
}



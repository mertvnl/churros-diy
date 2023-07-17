using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utilities;

namespace Game.Runtime 
{
    public class CustomerCreator : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private List<GameObject> customerPrefabs = new();

        private void OnEnable()
        {
            GameStateManager.Instance.OnEnterBeginningState.AddListener(CreateCustomer);
        }

        private void OnDisable()
        {
            GameStateManager.Instance.OnEnterBeginningState.RemoveListener(CreateCustomer);
        }

        private void CreateCustomer() 
        {
            GameObject customerPrefab = GetRandomCustomerPrefab();
            Customer customer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity).GetComponent<Customer>();
            customer.Initialize();
        }

        private GameObject GetRandomCustomerPrefab() 
        {
            List<GameObject> prefabs = new(customerPrefabs);
            prefabs.Shuffle();
            return prefabs[0];
        }
    }
}


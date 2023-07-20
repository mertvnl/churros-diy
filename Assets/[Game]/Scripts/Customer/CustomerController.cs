using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Helpers;

namespace Game.Runtime 
{
    public class CustomerController : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform cashierPoint;
        [SerializeField] private Transform exitPoint;
        [Space(15)]
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
            customer.Initialize(cashierPoint.position, exitPoint.position);
        }

        private GameObject GetRandomCustomerPrefab() 
        {
            List<GameObject> prefabs = new(customerPrefabs);
            prefabs.Shuffle();
            return prefabs[0];
        }
    }
}


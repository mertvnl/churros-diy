using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class CustomerStateMachine : MonoBehaviour
    {
        private Customer _customer;
        public Customer Customer => _customer == null ? _customer = GetComponent<Customer>() : _customer;       

        public CustomerStateBase CurrentState { get; private set; }

        private void OnEnable()
        {
            Customer.OnInitialized.AddListener(Initialize);
            GameStateManager.Instance.OnEnterChurrosDeliveryState.AddListener(() => SetState(new CustomerDeliveryState(this)));
        }

        private void OnDisable()
        {
            Customer.OnInitialized.RemoveListener(Initialize);
            GameStateManager.Instance.OnEnterChurrosDeliveryState.RemoveListener(() => SetState(new CustomerDeliveryState(this)));
        }

        private void Update()
        {
            CurrentState?.UpdateState();
        }

        public void SetState(CustomerStateBase customerState) 
        {
            CurrentState = customerState;
            CurrentState.EnterState();
        }

        private void Initialize() 
        {
            CurrentState = new CustomerWalkingCashierState(this);
            CurrentState.EnterState();
        }
    }
}

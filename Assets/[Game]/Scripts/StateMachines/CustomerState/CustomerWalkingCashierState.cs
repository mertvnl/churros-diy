using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class CustomerWalkingCashierState : CustomerStateBase
    {
        private CustomerMovement _movement;
        public CustomerMovement CustomerMovement => _movement == null ? _movement = StateMachine.GetComponent<CustomerMovement>() : _movement;
        public CustomerWalkingCashierState(CustomerStateMachine stateMachine) : base(stateMachine) { }

        private Vector3 _targetPosition;

        public override void EnterState()
        {
            _targetPosition = StateMachine.Customer.CashierPosition;
            CustomerMovement.MoveTowards(_targetPosition, onComplete: CompleteState);
        }        

        private void CompleteState() 
        {
            StateMachine.SetState(new CustomerOrderState(StateMachine));
        }
    }
}


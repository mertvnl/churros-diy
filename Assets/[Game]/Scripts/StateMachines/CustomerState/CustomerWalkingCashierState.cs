using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utilities;

namespace Game.Runtime 
{
    public class CustomerWalkingCashierState : CustomerStateBase
    {
        private CustomerMovement _movement;
        public CustomerMovement CustomerMovement => _movement == null ? _movement = StateMachine.GetComponent<CustomerMovement>() : _movement;

        private CustomerAnimation _customerAnimation;
        public CustomerAnimation CustomerAnimation => _customerAnimation == null ? _customerAnimation = StateMachine.GetComponent<CustomerAnimation>() : _customerAnimation;

        public CustomerWalkingCashierState(CustomerStateMachine stateMachine) : base(stateMachine) { }

        private Vector3 _targetPosition;

        public override void EnterState()
        {
            _targetPosition = StateMachine.Customer.CashierPosition;
            CustomerAnimation.SetTrigger(AnimatorStrings.Walk);
            CustomerMovement.MoveTowards(_targetPosition, onComplete: CompleteState);
        }        

        private void CompleteState() 
        {
            StateMachine.SetState(new CustomerOrderState(StateMachine));
        }
    }
}


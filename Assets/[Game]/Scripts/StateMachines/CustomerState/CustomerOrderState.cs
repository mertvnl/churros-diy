using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Helpers;
using Game.Managers;

namespace Game.Runtime 
{
    public class CustomerOrderState : CustomerStateBase
    {
        private CustomerAnimation _customerAnimation;
        public CustomerAnimation CustomerAnimation => _customerAnimation == null ? _customerAnimation = StateMachine.GetComponent<CustomerAnimation>() : _customerAnimation;

        private CustomerRotation _rotation;
        public CustomerRotation CustomerRotation => _rotation == null ? _rotation = StateMachine.GetComponent<CustomerRotation>() : _rotation;
        
        public CustomerOrderState(CustomerStateMachine stateMachine) : base(stateMachine) { }        

        public override void EnterState()
        {
            CustomerAnimation.SetTrigger(AnimatorStrings.Order);
            CustomerRotation.LookPosition(Vector3.zero, onComplete: () => 
            {
                GameStateManager.Instance.CurrentStateMachine.EnterNextState();
            });            
        }
    }
}


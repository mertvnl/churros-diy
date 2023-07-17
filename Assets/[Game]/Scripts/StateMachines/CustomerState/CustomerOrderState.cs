using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class CustomerOrderState : CustomerStateBase
    {
        private CustomerRotation _rotation;
        public CustomerRotation CustomerRotation => _rotation == null ? _rotation = StateMachine.GetComponent<CustomerRotation>() : _rotation;
        public CustomerOrderState(CustomerStateMachine stateMachine) : base(stateMachine) { }        

        public override void EnterState()
        {
            CustomerRotation.LookPosition(Vector3.zero);
        }
    }
}


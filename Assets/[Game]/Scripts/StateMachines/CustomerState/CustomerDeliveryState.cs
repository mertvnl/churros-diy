using Game.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class CustomerDeliveryState : CustomerStateBase
    {
        private CustomerAnimation _customerAnimation;
        public CustomerAnimation CustomerAnimation => _customerAnimation == null ? _customerAnimation = StateMachine.GetComponent<CustomerAnimation>() : _customerAnimation;

        public CustomerDeliveryState(CustomerStateMachine stateMachine) : base(stateMachine) { }

        public override void EnterState()
        {
            //Check from manager if it is a happy or sad animation.

            CustomerAnimation.SetTrigger(AnimatorStrings.Happy);
        }
    }
}
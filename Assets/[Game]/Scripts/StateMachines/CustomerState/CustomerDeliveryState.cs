using Game.Helpers;
using Game.Managers;
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
            CustomerAnimation.SetTrigger(ChurrosScoreManager.Instance.IsChurrosGood() ? AnimatorStrings.Happy : AnimatorStrings.Sad);
            StateMachine.GetComponent<CustomerReaction>().React(ChurrosScoreManager.Instance.IsChurrosGood() ? ReactionStrings.Happy : ReactionStrings.Angry);
        }
    }
}
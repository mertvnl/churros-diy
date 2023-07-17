using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public abstract class CustomerStateBase 
    {
        public CustomerStateMachine StateMachine { get; private set; }

        public CustomerStateBase(CustomerStateMachine stateMachine) 
        {
            StateMachine = stateMachine;
        }

        public virtual void EnterState() { }

        public virtual void UpdateState() { }
    }
}


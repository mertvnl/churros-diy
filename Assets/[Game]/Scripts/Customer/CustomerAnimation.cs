using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class CustomerAnimation : MonoBehaviour
    {
        private Animator _animator;
        public Animator Animator => _animator == null ? _animator = GetComponentInChildren<Animator>() : _animator;        

        public void SetTrigger(int id) 
        {
            Animator.SetTrigger(id);
        }
    }
}


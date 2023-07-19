using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class Topping : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponent<Rigidbody>() : _rigidbody;

        public bool IsActive { get; private set; }

        public void Initialize() 
        {
            IsActive = true;
            Rigidbody.isKinematic = false;         
        }

        private void OnCollisionEnter(Collision collision)
        {
            Stick();
        }

        private void Stick() 
        {
            if (!IsActive)
                return;

            IsActive = false;
            Rigidbody.isKinematic = true;
        }
    }
}

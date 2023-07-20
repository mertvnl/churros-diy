using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class Topping : MonoBehaviour
    {
        private PoolObject _poolObject;
        private PoolObject PoolObject => _poolObject == null ? _poolObject = GetComponent<PoolObject>() : _poolObject;

        private Rigidbody _rigidbody;
        private Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponent<Rigidbody>() : _rigidbody;

        public bool IsActive { get; private set; }

        private void OnEnable()
        {
            PoolObject.OnInitialized.AddListener(Initialize);
        }

        private void OnDisable()
        {
            PoolObject.OnInitialized.RemoveListener(Initialize);
        }

        private void Initialize() 
        {
            IsActive = true;
            Rigidbody.isKinematic = false;
            ToppingManager.Instance.AddTopping(this);            
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

using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class PoolParticle : PoolObject
    {
        private ParticleSystem _particleSystem;
        private ParticleSystem ParticleSystem => _particleSystem == null ? _particleSystem = GetComponent<ParticleSystem>() : _particleSystem;

        public override void Initialize()
        {
            ParticleSystem.Play();
            base.Initialize();
        }

        private void OnParticleSystemStopped()
        {
            PoolingManager.Instance.DestroyPoolObject(this);
        }
    }
}


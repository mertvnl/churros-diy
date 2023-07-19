using UnityEngine;

namespace Game.Actors.PoolingSystem 
{
    public class PoolParticle : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private ParticleSystem ParticleSystem => _particleSystem ??= GetComponent<ParticleSystem>();

        private PoolObject _poolObject;
        private PoolObject PoolObject => _poolObject ??= GetComponentInParent<PoolObject>();

        private void Awake()
        {
            SetupParticleSystem();
        }

        private void OnEnable()
        {
            ParticleSystem.Play();
        }

        private void OnParticleSystemStopped()
        {
            if (PoolObject == null)
                return;

            PoolingManager.Instance.DestroyPoolObject(PoolObject.gameObject);
        }

        private void SetupParticleSystem()
        {
            var main = ParticleSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
            main.playOnAwake = false;
        }
    }

}

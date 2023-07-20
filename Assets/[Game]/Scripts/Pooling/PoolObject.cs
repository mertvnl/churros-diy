using Game.Enums;
using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime 
{
    public class PoolObject : MonoBehaviour
    {
        public Vector3 DefultScale { get; protected set; }
        public UnityEvent OnInitialized { get; } = new UnityEvent();
        public UnityEvent OnDisposed { get; } = new UnityEvent();
        public PoolID PoolID { get => poolID; protected set => poolID = value; }

        [SerializeField] private PoolID poolID;

        protected virtual void Awake()
        {
            DefultScale = transform.localScale;
        }

        private void OnEnable()
        {
            LevelManager.Instance.OnLevelLoaded.AddListener(OnLevelFinished);
        }

        private void OnDisable()
        {
            LevelManager.Instance.OnLevelLoaded.RemoveListener(OnLevelFinished);
        }

        public virtual void Initialize()
        {
            OnInitialized.Invoke();
        }

        public virtual void Dispose()
        {
            transform.localScale = DefultScale;
            OnDisposed.Invoke();
        }

        private void OnLevelFinished() 
        {
            PoolingManager.Instance.DestroyPoolObject(this);
        }
    }
}


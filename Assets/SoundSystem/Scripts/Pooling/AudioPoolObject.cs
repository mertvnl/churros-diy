using UnityEngine;

namespace Game.Actors.PoolingSystem
{
    public class AudioPoolObject : MonoBehaviour
    {
        public int PoolID { get; private set; }

        private Vector3 _defaultScale;

        private void Awake()
        {
            _defaultScale = transform.localScale;
        }        

        public void Initialize(int poolID)
        {
            PoolID = poolID;
        }

        public void Dispose()
        {
            transform.localScale = _defaultScale;
        }
    }
}

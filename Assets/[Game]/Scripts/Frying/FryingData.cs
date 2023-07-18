using UnityEngine;

namespace Game.Models
{
    [System.Serializable]
    public class FryingData
    {
        [field: SerializeField] public float BurnedTime { get; private set; } = 3f;
        [field: SerializeField] public Gradient FryingGradient { get; private set; }

        public float FryingDuration;
        public bool IsFryingStarted = false;
    }
}



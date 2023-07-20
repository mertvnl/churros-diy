using Game.Helpers;
using System;
using UnityEngine;

namespace Game.Runtime
{
    public class CustomerReaction : MonoBehaviour
    {
        public SerializableReactionDictionary reactions;
        public void React(int reactionId)
        {
            reactions[reactionId].Play();
        }
    }

    [Serializable]
    public class SerializableReactionDictionary: SDictionary<int, ParticleSystem> { }
}


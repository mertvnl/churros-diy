using Game.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    [CreateAssetMenu(menuName = "Data/Create AudioDatabase")]
    public class AudioDatabase : ScriptableObject
    {
        public SerializedAudioDictionary audioDictionary;
    }

    [Serializable]
    public class SerializedAudioDictionary : SDictionary<AudioID, AudioClip> { }
}
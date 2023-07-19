using UnityEngine;

namespace Game.Actors
{
    public abstract class MusicBaseActor : MonoBehaviour
    {
        [SerializeField] protected AudioClip[] audioClips;

        protected virtual AudioClip GetAudioClip()
        {
            if (audioClips.Length == 0)
                return null;

            if (audioClips.Length == 1)
                return audioClips[0];

            return audioClips[Random.Range(0, audioClips.Length)];
        }
    }
}
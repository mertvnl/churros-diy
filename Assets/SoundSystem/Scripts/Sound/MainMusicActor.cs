using Game.Managers;
using UnityEngine;

namespace Game.Actors
{
    public class MainMusicActor : MusicBaseActor
    {
        private void Start()
        {
            AudioClip clip = GetAudioClip();

            if (clip == null)
                return;

            SoundManager.Instance.PlayMusic(clip);
        }
    }
}
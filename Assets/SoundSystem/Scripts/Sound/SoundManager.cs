using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using Game.GlobalVariables;
using Game.Models;
using System;
using UnityEngine.Events;

namespace Game.Managers
{
    public class SoundManager : Singleton<SoundManager>
    {
        #region Properties
        [field: SerializeField] public AudioDatabase AudioDatabase { get; private set; }

        private AudioSource _musicSource;
        private AudioSource MusicSource => _musicSource == null ? _musicSource = GetComponent<AudioSource>() : _musicSource;

        private Dictionary<AudioID, int> _pitchCountsByID = new();

        private const float INCREASE_AMOUNT = 0.1f;

        public bool IsSoundActive
        {
            get
            {
                return PlayerPrefs.GetInt(SOUND_EFFECT_SAVE_ID, 1) == 1;
            }

            private set
            {
                PlayerPrefs.SetInt(SOUND_EFFECT_SAVE_ID, value ? 1 : 0);

                if (!value)
                    StopAllContinuousSounds();

                OnSoundEffectStatusChanged.Invoke(value);
            }
        }
        public bool IsMusicActive
        {
            get
            {
                return PlayerPrefs.GetInt(MUSIC_SAVE_ID, 1) == 1;
            }

            private set
            {
                PlayerPrefs.SetInt(MUSIC_SAVE_ID, value ? 1 : 0);

                if (!value)
                    StopMusic();

                OnMusicStatusChanged.Invoke(value);
            }
        }

        public UnityEvent<bool> OnMusicStatusChanged { get; private set; } = new();
        public UnityEvent<bool> OnSoundEffectStatusChanged { get; private set; } = new();
        #endregion

        #region Private Variables
        [Header("Settings")]
        [Range(0, 1)]
        [SerializeField]
        private float generalVolume;

        private Dictionary<AudioID, AudioSource> _audioSources = new();
        private List<AudioID> _audioIDs = new();
        #endregion

        #region Constant Variables
        private const float LOOP_VOLUME_DELAY = 0.2f;
        private const float OVERLAP_WAIT_DELAY = 0.1f;
        private const float MIN_PITCH = 0.1f;
        private const float MAX_PITCH = 3f;
        private const string SOUND_EFFECT_SAVE_ID = "SoundEffectSave";
        private const string MUSIC_SAVE_ID = "MusicSave";
        #endregion

        private void OnEnable()
        {
            GameManager.Instance.OnLevelCompleted.AddListener(OnLevelComplete);
        }

        private void OnDisable()
        {
            GameManager.Instance.OnLevelCompleted.RemoveListener(OnLevelComplete);
        }

        private void OnLevelComplete(bool obj)
        {
            StopAllContinuousSounds();
        }

        public void PlayMusic(AudioClip clip)
        {
            if (!IsMusicActive)
                return;

            if (MusicSource.isPlaying && MusicSource.clip == clip)
                return;

            if (MusicSource.clip != clip)
                StopMusic(onStopped: Play);

            void Play()
            {
                MusicSource.clip = clip;
                MusicSource.volume = 0;
                MusicSource.loop = true;
                MusicSource.Play();

                DOTween.To(() => MusicSource.volume, x => MusicSource.volume = x, generalVolume, LOOP_VOLUME_DELAY);
            }
        }

        [Button("Test Sound")]
        public void PlaySound(AudioID audioId, float pitch = 1, bool increasePitch = false)
        {
            if (!IsSoundActive)
                return;

            if (!CanPlaySoundEffect(audioId))
                return;

            AudioClip clip = GetClipByID(audioId);

            if (clip == null)
                return;

            AddAudioID(audioId);

            if (pitch <= MIN_PITCH)
                pitch = MIN_PITCH;
            else if (pitch >= MAX_PITCH)
                pitch = MAX_PITCH;

            StartCoroutine(PlaySoundCo(audioId, clip, pitch, increasePitch));
        }

        private IEnumerator PlaySoundCo(AudioID audioId, AudioClip clip, float pitch, bool increasePitch)
        {
            GameObject sourceObject = AudioPoolingManager.Instance.InstantiateFromPool(PoolingStrings.AudioSource, transform);

            if (sourceObject == null)
                yield break;

            if (!sourceObject.TryGetComponent(out AudioSource source))
                yield break;

            if (increasePitch)
                AddPitchCollection(audioId);

            source.clip = clip;
            source.volume = generalVolume;

            float pitchAmount = pitch;
            if (increasePitch)
            {
                pitchAmount += GetPitchSoundCount(audioId) * INCREASE_AMOUNT;
                pitchAmount = Mathf.Clamp(pitchAmount, MIN_PITCH, MAX_PITCH);
            }

            source.pitch = pitchAmount;
            source.Play();

            yield return new WaitForSeconds(clip.length);

            AudioPoolingManager.Instance.DestroyPoolObject(source.gameObject);
        }

        [Button("Test Continuous Sound")]
        public void PlayContinuousSound(AudioID audioId)
        {
            if (!IsSoundActive)
                return;

            if (_audioSources.ContainsKey(audioId))
                return;

            AudioClip clip = GetClipByID(audioId);

            if (clip == null)
                return;

            AudioSource source = AudioPoolingManager.Instance.InstantiateFromPool(PoolingStrings.AudioSource, transform).GetComponent<AudioSource>();
            _audioSources.Add(audioId, source);
            source.loop = true;
            source.clip = clip;
            source.volume = 0;
            source.Play();
            DOTween.To(() => source.volume, x => source.volume = x, generalVolume, LOOP_VOLUME_DELAY);
        }

        public void StopContinuousSound(AudioID audioId)
        {
            _audioSources.TryGetValue(audioId, out AudioSource source);

            if (source == null)
                return;

            DOTween.To(() => source.volume, x => source.volume = x, 0, LOOP_VOLUME_DELAY)
                .OnComplete(() =>
                {
                    source.Stop();
                    AudioPoolingManager.Instance.DestroyPoolObject(source.gameObject);
                });

            _audioSources.Remove(audioId);
        }

        public void StopSound(AudioID audioId)
        {
            _audioSources.TryGetValue(audioId, out AudioSource source);

            if (source == null)
                return;

            source.Stop();
            AudioPoolingManager.Instance.DestroyPoolObject(source.gameObject);
            _audioSources.Remove(audioId);
        }

        private void AddPitchCollection(AudioID audioId)
        {
            string tweenID = GetInstanceID().ToString() + audioId;
            DOTween.Kill(tweenID);
            DOVirtual.DelayedCall(2f, () => ClearPitchCollection(audioId)).SetId(tweenID);

            if (!_pitchCountsByID.ContainsKey(audioId))
            {
                _pitchCountsByID.Add(audioId, 1);
            }
            else
            {
                _pitchCountsByID[audioId] += 1;
            }
        }

        private void ClearPitchCollection(AudioID audioId)
        {
            string tweenID = GetInstanceID().ToString() + audioId;
            DOTween.Kill(tweenID);

            if (!_pitchCountsByID.ContainsKey(audioId))
                return;

            _pitchCountsByID[audioId] = 0;
        }

        private AudioClip GetClipByID(AudioID audioId)
        {
            AudioDatabase.audioDictionary.TryGetValue(audioId, out AudioClip clip);

            if (clip == null)
            {
                Debug.LogError("There is no audio with name of " + audioId, AudioDatabase);
                return null;
            }

            return clip;
        }

        public void SetMusicStatus(bool status)
        {
            IsMusicActive = status;
        }

        public void SetSoundStatus(bool status)
        {
            IsSoundActive = status;
        }

        private bool CanPlaySoundEffect(AudioID audioID)
        {
            if (_audioIDs.Contains(audioID))
                return false;

            return true;
        }

        private int GetPitchSoundCount(AudioID audioID)
        {
            if (!_pitchCountsByID.ContainsKey(audioID))
                return 0;

            return _pitchCountsByID[audioID];
        }

        private void StopAllContinuousSounds(params object[] args)
        {
            if (_audioSources.Count == 0 || _audioSources == null)
                return;

            foreach (KeyValuePair<AudioID, AudioSource> sound in _audioSources)
                StopContinuousSound(sound.Key);
        }

        private void StopMusic(Action onStopped = null)
        {
            DOTween.To(() => MusicSource.volume, x => MusicSource.volume = x, 0, LOOP_VOLUME_DELAY)
                .OnComplete(() =>
                {
                    MusicSource.Stop();
                    MusicSource.clip = null;
                    onStopped?.Invoke();
                });
        }

        private void AddAudioID(AudioID audioID)
        {
            if (_audioIDs.Contains(audioID))
                return;

            _audioIDs.Add(audioID);
            DOVirtual.DelayedCall(OVERLAP_WAIT_DELAY, RemoveAudioID);

            void RemoveAudioID()
            {
                if (!_audioIDs.Contains(audioID))
                    return;

                _audioIDs.Remove(audioID);
            }
        }
    }
}
using DG.Tweening;
using Game.Interfaces;
using Game.Managers;
using Game.Models;
using Game.Props;
using UnityEngine;

namespace Game.Runtime
{
    public class Fryable : MonoBehaviour, IFryable
    {
        [field: SerializeField] public FryingData FryingData { get; private set; } = new();

        [SerializeField]
        private MeshRenderer mesh;
        [SerializeField]
        private ParticleSystem fryingParticle;

        private Tween _fryingTween;

        [Sirenix.OdinInspector.Button]
        public void StartFrying()
        {
            fryingParticle.Play();
            SoundManager.Instance.PlayContinuousSound(AudioID.Frying);
            UIManager.Instance.HidePanel(Enums.PanelID.TapAndHoldPanel);
            Fry();
        }

        public void Fry()
        {
            if (FryingData.IsFryingStarted)
            {
                _fryingTween.Play();
                return;
            }

            FryingData.IsFryingStarted = true;
            _fryingTween = mesh.material.DOGradientColor(FryingData.FryingGradient, FryingData.BurnedTime)
                .OnUpdate(()=> {
                    FryingData.FryingDuration += Time.deltaTime;
                    FryingOil.OnFry?.Invoke(FryingData);
                    HapticManager.PlayHaptic(Lofelt.NiceVibrations.HapticPatterns.PresetType.LightImpact);
                });
        }

        public void StopFrying()
        {
            _fryingTween.Pause();
            fryingParticle.Stop();
            SoundManager.Instance.StopContinuousSound(AudioID.Frying);

        }
    }
}

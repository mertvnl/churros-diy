using Game.Models;
using Game.Props;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Game.Runtime
{
    public class FryingSlider : MonoBehaviour
    {
        public static FryingSlider Instance = null;
        public Slider Slider => slider;

        [Header("References")]
        [SerializeField]
        private Slider slider;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            FryingOil.OnFry += Increase;
            LevelManager.Instance.OnLevelFinished.AddListener(ResetSlider);
        }

        private void OnDisable()
        {
            FryingOil.OnFry -= Increase;
            LevelManager.Instance.OnLevelFinished.RemoveListener(ResetSlider);
        }

        private void Increase(FryingData fryingData)
        {
            slider.value = fryingData.FryingDuration / fryingData.BurnedTime;
        }

        private void ResetSlider()
        {
            slider.value = 0;
        }
    }
}


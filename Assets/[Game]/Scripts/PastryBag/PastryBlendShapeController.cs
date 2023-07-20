using DG.Tweening;
using Game.Managers;
using Game.Runtime;
using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastryBlendShapeController : MonoBehaviour
{
    private PastryBag _pastryBag;
    private PastryBag PastryBag => _pastryBag == null ? _pastryBag = GetComponent<PastryBag>() : _pastryBag;

    [SerializeField] private SkinnedMeshRenderer pastryMesh;

    [Header("Settings")]
    [SerializeField] private float blendSpeed = 5f;
    [SerializeField] private Ease blendEase = Ease.InOutBounce;

    private Tween _blendTween;

    private const int BLENDSHAPE_INDEX = 0;
    private const int BLENDSHAPE_MAX_WEIGHT = 111;
    private const int BLENDSHAPE_MIN_WEIGHT = 0;

    private void OnEnable()
    {
        PastryBag.OnInputStart.AddListener(Squeeze);
        PastryBag.OnInputStop.AddListener(Release);
    }

    private void OnDisable()
    {
        PastryBag.OnInputStart.RemoveListener(Squeeze);
        PastryBag.OnInputStop.RemoveListener(Release);
    }

    public void Squeeze()
    {
        UpdateBlendShape(BLENDSHAPE_MAX_WEIGHT);
        SoundManager.Instance.PlaySound(Game.Models.AudioID.Squeezing);
        HapticManager.PlayHaptic(Lofelt.NiceVibrations.HapticPatterns.PresetType.MediumImpact);
    }

    public void Release()
    {
        UpdateBlendShape(BLENDSHAPE_MIN_WEIGHT);
    }

    private void UpdateBlendShape(float to)
    {
        float current = pastryMesh.GetBlendShapeWeight(BLENDSHAPE_INDEX);
        _blendTween?.Kill();
        _blendTween = DOTween.To(() => current, x => current = x, to, blendSpeed).SetSpeedBased().SetEase(blendEase)
            .OnUpdate(() => {
                pastryMesh.SetBlendShapeWeight(BLENDSHAPE_INDEX, current);
            });
    }
}
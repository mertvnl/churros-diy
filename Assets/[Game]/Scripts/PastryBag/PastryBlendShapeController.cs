using DG.Tweening;
using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastryBlendShapeController : MonoBehaviour
{
    private LeanSelectableByFinger _leanSelectable;
    private LeanSelectableByFinger LeanSelectable => _leanSelectable == null ? _leanSelectable = GetComponent<LeanSelectableByFinger>() : _leanSelectable;

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
        LeanSelectable.OnSelectedFinger.AddListener(Squeeze);
        LeanSelectable.OnSelectedFingerUp.AddListener(Release);
    }

    private void OnDisable()
    {
        LeanSelectable.OnSelectedFinger.RemoveListener(Squeeze);
        LeanSelectable.OnSelectedFingerUp.RemoveListener(Release);
    }

    public void Squeeze(LeanFinger arg)
    {
        UpdateBlendShape(BLENDSHAPE_MAX_WEIGHT);
    }

    public void Release(LeanFinger arg)
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
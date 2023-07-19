using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoadingPanel : EasyPanel
{
    private Image loadingImage;
    public Image LoadingImage { get { return loadingImage == null ? loadingImage = GetComponentInChildren<Image>() : loadingImage; } }

    private void Awake()
    {
        ShowPanel();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        LevelManager.Instance.OnLevelLoadingStarted.AddListener(FirstPhaseAnimation);
        LevelManager.Instance.OnLevelLoaded.AddListener(SecondPhaseAnimation);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        LevelManager.Instance.OnLevelLoadingStarted.RemoveListener(FirstPhaseAnimation);
        LevelManager.Instance.OnLevelLoaded.RemoveListener(SecondPhaseAnimation);
    }

    private void FirstPhaseAnimation()
    {
        RectTransform rect = LoadingImage.transform as RectTransform;

        rect.DOAnchorPosY(-600f, 1f);
    }

    private void SecondPhaseAnimation()
    {
        RectTransform rect = LoadingImage.transform as RectTransform;

        rect.DOAnchorPosY(2000f, 1f);
    }
}

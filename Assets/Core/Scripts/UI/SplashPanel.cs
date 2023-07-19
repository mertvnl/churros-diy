using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashPanel : EasyPanel
{
    protected override void OnEnable()
    {
        base.OnEnable();
        LevelManager.Instance.OnLevelLoaded.AddListener(OnLevelLoaded);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        LevelManager.Instance.OnLevelLoaded.RemoveListener(OnLevelLoaded);
    }

    private void Awake()
    {
        ShowPanel();
    }

    private void OnLevelLoaded()
    {
        HidePanel();
        Destroy(gameObject);
    }
}

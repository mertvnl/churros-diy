using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePanel : EasyPanel
{
    protected override void OnEnable()
    {
        base.OnEnable();
        LevelManager.Instance.OnLevelStarted.AddListener(ShowPanelAnimated);
        LevelManager.Instance.OnLevelLoaded.AddListener(HidePanel);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        LevelManager.Instance.OnLevelStarted.RemoveListener(ShowPanelAnimated);
        LevelManager.Instance.OnLevelLoaded.RemoveListener(HidePanel);
    }
}

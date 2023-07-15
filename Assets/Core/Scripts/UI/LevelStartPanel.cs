using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartPanel : EasyPanel
{
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStarted.AddListener(HidePanelAnimated);
        LevelManager.Instance.OnLevelLoaded.AddListener(ShowPanelAnimated);
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStarted.RemoveListener(HidePanelAnimated);
        LevelManager.Instance.OnLevelLoaded.RemoveListener(ShowPanelAnimated);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailPanel : EasyPanel
{
    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.Instance.OnLevelCompleted.AddListener(TogglePanel);
        LevelManager.Instance.OnLevelLoaded.AddListener(HidePanel);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameManager.Instance.OnLevelCompleted.RemoveListener(TogglePanel);
        LevelManager.Instance.OnLevelLoaded.RemoveListener(HidePanel);
    }

    private void Awake()
    {
        HidePanel();
    }

    private void TogglePanel(bool isSuccess)
    {
        if (!isSuccess)
            ShowPanelAnimated();
    }

    public void RestartLevelButton()
    {
        HidePanelAnimated();
        LevelManager.Instance.RestartLevel();
    }
}

using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartPanel : EasyPanel
{
    protected override void OnEnable()
    {
        base.OnEnable();
        LevelManager.Instance.OnLevelStarted.AddListener(HidePanelAnimated);
        GameStateManager.Instance.OnEnterCustomerOrderState.AddListener(ShowPanelAnimated);        
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        LevelManager.Instance.OnLevelStarted.RemoveListener(HidePanelAnimated);
        GameStateManager.Instance.OnEnterCustomerOrderState.RemoveListener(ShowPanelAnimated);        
    }
}

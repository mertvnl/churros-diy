using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartPanel : EasyPanel
{
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStarted.AddListener(HidePanelAnimated);
        GameStateManager.Instance.OnEnterCustomerOrderState.AddListener(ShowPanelAnimated);        
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStarted.RemoveListener(HidePanelAnimated);
        GameStateManager.Instance.OnEnterCustomerOrderState.RemoveListener(ShowPanelAnimated);        
    }
}

using Game.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces 
{
    public interface IPanel
    {
        PanelID PanelID { get; }        
        void ShowPanelAnimated();
        void HidePanelAnimated();
    }
}

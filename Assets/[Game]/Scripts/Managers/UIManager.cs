using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enums;
using Game.Interfaces;

namespace Game.Managers 
{
    public class UIManager : Singleton<UIManager>
    {
        public Dictionary<PanelID, IPanel> PanelsByID { get; private set; } = new Dictionary<PanelID, IPanel>();

        public void ShowPanel(PanelID panelID)
        {
            if (!PanelsByID.ContainsKey(panelID))
                return;

            PanelsByID[panelID].ShowPanelAnimated();
        }

        public void HidePanel(PanelID panelID)
        {
            if (!PanelsByID.ContainsKey(panelID))
                return;

            PanelsByID[panelID].HidePanelAnimated();
        }

        public void HideAllPanels()
        {
            foreach (var panel in PanelsByID.Values)
            {
                panel.HidePanelAnimated();
            }
        }

        public void AddPanel(IPanel panel)
        {
            if (PanelsByID.ContainsKey(panel.PanelID))
                return;

            PanelsByID.Add(panel.PanelID, panel);
        }

        public void RemovePanel(IPanel panel)
        {
            if (!PanelsByID.ContainsKey(panel.PanelID))
                return;

            PanelsByID.Remove(panel.PanelID);
        }
    }
}

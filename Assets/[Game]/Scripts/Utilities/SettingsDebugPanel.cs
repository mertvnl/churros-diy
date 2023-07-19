using Game.Managers;
using UnityEngine;

namespace Game.Utilities
{
    public class SettingsDebugPanel : EasyPanel
    {
        public void SetSoundStatus(bool isActive)
        {
            SoundManager.Instance.SetSoundStatus(isActive);
        }

        public void SetHapticStatus(bool isActive)
        {
            HapticManager.Instance.SetHapticStatus(isActive);
        }
    }
}


using Lofelt.NiceVibrations;

namespace Game.Managers
{
    public class HapticManager : Singleton<HapticManager>
    {
        public static bool IsActive => SaveManager.GetBool(HAPTIC_SAVE_KEY, true);

        private const string HAPTIC_SAVE_KEY = "HapticSave";

        /// <summary>
        /// Enables or disables haptics.
        /// </summary>
        /// <param name="status"></param>
        public void SetHapticStatus(bool status)
        {
            SaveManager.SetBool(HAPTIC_SAVE_KEY, status);
        }

        /// <summary>
        /// Plays haptic by given type.
        /// </summary>
        public static void PlayHaptic(HapticPatterns.PresetType hapticType)
        {
            if (!IsActive)
                return;

            HapticPatterns.PlayPreset(hapticType);
        }
    }
}
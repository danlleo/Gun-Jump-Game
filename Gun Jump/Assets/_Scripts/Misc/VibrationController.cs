using UnityEngine;

namespace _Scripts.Misc
{
    public static class VibrationController 
    {
        public static void TriggerVibration()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                Handheld.Vibrate();
        }
    }
}

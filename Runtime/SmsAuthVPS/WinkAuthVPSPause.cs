using Agava.Wink;
using UnityEngine;
using UnityEngine.Scripting;

namespace KinDzaDza.RateUs.SmsAuthVPS
{
    [Preserve]
    public static class WinkAuthVPSPause
    {
        [Preserve]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Register()
        {
            RateUsPopupTimer.IsPaused = () =>
            {
                WinkSignInHandlerUI wink = WinkSignInHandlerUI.Instance;
                return wink == null || wink.IsAnyWindowEnabled;
            };
        }
    }
}

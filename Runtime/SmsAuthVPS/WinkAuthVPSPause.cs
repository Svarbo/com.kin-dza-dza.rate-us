using Agava.Wink;
using UnityEngine;

namespace KinDzaDza.RateUs.SmsAuthVPS
{
    public static class WinkAuthVPSPause
    {
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

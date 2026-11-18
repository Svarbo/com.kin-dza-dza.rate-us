using KinDzaDzaGames.AdvertisementPlugin;
using UnityEngine;
using UnityEngine.Scripting;

namespace KinDzaDza.RateUs.Advertisement
{
    [Preserve]
    public static class AdvertisementBannerPause
    {
        [Preserve]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Register()
        {
            RateUsPopupTimer.IsAdvertisementBannerPaused = () =>
                AdvertisementController.Instance != null && AdvertisementController.Instance.BannerShown;
        }
    }
}

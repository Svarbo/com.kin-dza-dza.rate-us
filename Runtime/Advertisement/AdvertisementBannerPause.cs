using KinDzaDzaGames.AdvertisementPlugin;
using UnityEngine;

namespace KinDzaDza.RateUs.Advertisement
{
    public static class AdvertisementBannerPause
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Register()
        {
            RateUsPopupTimer.IsAdvertisementBannerPaused = () =>
                AdvertisementController.Instance != null && AdvertisementController.Instance.BannerShown;
        }
    }
}

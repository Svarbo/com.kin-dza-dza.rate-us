using KinDzaDzaGames.AdvertisementPlugin;
using UnityEngine;
using UnityEngine.Scripting;

namespace KinDzaDza.RateUs.Advertisement
{
    [Preserve]
    public class RateUsAdvertisementBlocker : IBannerBlocker, IInterstitialBlocker
    {
        private static RateUsAdvertisementBlocker _blocker;
        private bool _rateUsWindowOpen;

        public bool BannerDisplayBlocked => _rateUsWindowOpen;
        public bool InterstitialDisplayBlocked => _rateUsWindowOpen;

        public void RemoveRestriction() { }

        [Preserve]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Register()
        {
            if (RateUsPopup.Instance == null)
            {
                return;
            }

            _blocker = new RateUsAdvertisementBlocker();

            RateUsPopup.Instance.Shown += _blocker.Block;
            RateUsPopup.Instance.Closed += _blocker.Unblock;
        }

        private void Block()
        {
            _rateUsWindowOpen = true;
            AdvertisementController advertisement = AdvertisementController.Instance;

            if (advertisement == null)
            {
                return;
            }

            advertisement.HideBanner();
            advertisement.AddInterstitialBlocker(this);
            advertisement.InterstitialPlayer.Suspend();
        }

        private void Unblock()
        {
            _rateUsWindowOpen = false;
            AdvertisementController advertisement = AdvertisementController.Instance;

            if (advertisement == null)
            {
                return;
            }

            advertisement.InterstitialPlayer.Continue();
            advertisement.ShowBanner();
        }
    }
}

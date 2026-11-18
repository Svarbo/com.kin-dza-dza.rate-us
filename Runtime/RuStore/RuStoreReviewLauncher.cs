using System;
using RuStore.Review;
using UnityEngine;
using UnityEngine.Scripting;

namespace KinDzaDza.RateUs.RuStore
{
    [Preserve]
    public class RuStoreReviewLauncher
    {
        public event Action<bool> CompleteOpening;

        public void TryOpenRuStorePopup()
        {
#if UNITY_EDITOR
            CompleteOpening?.Invoke(false);
#else
            RuStoreReviewManager manager = RuStoreReviewManager.Instance;

            if (manager.IsInitialized == false && manager.Init() == false)
            {
                Debug.LogError("RuStore Review Init failed");
                CompleteOpening?.Invoke(false);

                return;
            }

            manager.RequestReviewFlow(
                onFailure: error =>
                {
                    Debug.LogError($"RuStore RequestReviewFlow: {error.name} {error.description}");
                    CompleteOpening?.Invoke(false);
                },
                onSuccess: () =>
                {
                    manager.LaunchReviewFlow(
                        onFailure: error =>
                        {
                            Debug.LogError($"RuStore LaunchReviewFlow: {error.name} {error.description}");
                            CompleteOpening?.Invoke(false);
                        },
                        onSuccess: () => CompleteOpening?.Invoke(true));
                });
#endif
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Register()
        {
            RateUsPopup.LaunchRuStore = popup =>
            {
                RuStoreReviewLauncher launcher = new RuStoreReviewLauncher();

                launcher.CompleteOpening += opened =>
                {
                    if (opened)
                    {
                        popup.Hide();
                    }
                    else
                    {
                        popup.OpenStorePage();
                    }
                };

                launcher.TryOpenRuStorePopup();
            };
        }
    }
}

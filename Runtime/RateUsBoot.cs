using System.Collections;
using System.Threading.Tasks;
using AdsAppView.Utility;
using UnityEngine;

namespace KinDzaDza.RateUs
{
    public class RateUsBoot : MonoBehaviour
    {
        [SerializeField] private BuildVersionHolder _buildVersionHolder;
        [SerializeField] private RateUsPopup _rateUsPopup;
        [SerializeField] private RateUsPopupTimer _rateUsPopupTimer;
        [SerializeField] private string _serverPath = "gamesrtkbd.tech";

        private RateUsConfigProvider _rateUsConfigProvider;

        private bool IsRateUsShown() => PlayerPrefs.GetInt(Constants.RateUsShownKey, Constants.FalseValueInt) == Constants.TrueValueInt;

        private IEnumerator Start()
        {
            if (!IsRateUsShown())
            {
                yield return new WaitUntil(() => AdsAppAPI.Instance != null && AdsAppAPI.Instance.Initialized);

                _rateUsConfigProvider = new RateUsConfigProvider(_buildVersionHolder.StoreName, _serverPath);

                Task<RateUsConfig> getConfigTask = _rateUsConfigProvider.GetConfig();
                yield return new WaitUntil(() => getConfigTask.IsCompleted);

                if (getConfigTask.IsFaulted || getConfigTask.Result == null || string.IsNullOrEmpty(getConfigTask.Result.StoreLink))
                {
                    Debug.LogException(getConfigTask.Exception);
                    yield break;
                }

                RateUsConfig config = getConfigTask.Result;

                _rateUsPopupTimer.Construct(config.TriggerTimeSeconds);
                _rateUsPopup.Construct(config.StoreLink, _buildVersionHolder.StoreName);
            }
        }
    }
}

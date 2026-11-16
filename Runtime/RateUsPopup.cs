using System;
using System.Collections;
using AdsAppView.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace KinDzaDza.RateUs
{
    public class RateUsPopup : MonoBehaviour
    {
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _refuseButton;
        [SerializeField] private Image _popupBackground;
        [SerializeField] private RateUsPopupTimer _rateUsPopupTimer;

        private string _storeLink;
        private Store _store;

        public static RateUsPopup Instance { get; private set; }

        public event Action Shown;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);

            _popupBackground.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
            _refuseButton.onClick.AddListener(OnRefuseButtonClicked);
        }

        private void OnDisable()
        {
            _confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
            _refuseButton.onClick.RemoveListener(OnRefuseButtonClicked);

            _rateUsPopupTimer.TimerExpired -= OnRateUsTimerExpired;
        }

        public void Construct(string storeLink, Store store)
        {
            Debug.LogError("Construct");
            _storeLink = storeLink;
            _store = store;

            _rateUsPopupTimer.TimerExpired += OnRateUsTimerExpired;
        }

        public bool TryShow()
        {
            bool isContainsStoreLink = !string.IsNullOrEmpty(_storeLink);

            if (isContainsStoreLink)
            {
                Show();
            }

            return isContainsStoreLink;
        }

        public void Hide()
        {
            _popupBackground.gameObject.SetActive(false);
        }

        private void Show()
        {
            _popupBackground.gameObject.SetActive(true);
            PlayerPrefs.SetInt(Constants.RateUsShownKey, Constants.TrueValueInt);

            if (_rateUsPopupTimer.IsExpired == false)
            {
                _rateUsPopupTimer.Stop();
            }

            Shown?.Invoke();
        }

        private void OnRateUsTimerExpired()
        {
            Debug.LogError("OnRateUsTimerExpired");
            TryShow();
        }

        private void OnConfirmButtonClicked()
        {
            switch (_store)
            {
                case Store.Google:
                    LaunchGooglePlayReview();
                    break;
                case Store.RuStore:
                    break;
                default:
                    TryOpenStorePage();
                    break;
            }

            Hide();
        }

        private void OnRefuseButtonClicked()
        {
            Hide();
        }

        private IEnumerator LaunchGooglePlayReview()
        {
#if GOOGLE_PLAY
            ReviewManager reviewManager = new ReviewManager();
            PlayAsyncOperation<PlayReviewInfo, ReviewErrorCode> request = reviewManager.RequestReviewFlow();
            yield return request;

            if (request.Error != ReviewErrorCode.NoError)
            {
                OpenStorePage();
                yield break;
            }

            PlayAsyncOperation<VoidResult, ReviewErrorCode> launch = reviewManager.LaunchReviewFlow(request.GetResult());
            yield return launch;

            if (launch.Error != ReviewErrorCode.NoError)
            {
                OpenStorePage();
            }
            else
            {
                Hide();
            }
#else
            yield return null;
#endif
        }

#if RU_STORE
#endif

        private bool TryOpenStorePage()
        {
            bool isLinkEmpty = string.IsNullOrEmpty(_storeLink);

            if (!isLinkEmpty)
            {
                Application.OpenURL(_storeLink);
            }

            Hide();

            return !isLinkEmpty;
        }
    }
}

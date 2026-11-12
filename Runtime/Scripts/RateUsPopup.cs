using UnityEngine;
using UnityEngine.UI;

namespace KinDzaDza.RateUs
{
    public class RateUsPopup : MonoBehaviour
    {
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _refuseButton;

        private void OnEnable()
        {
            _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
            _refuseButton.onClick.AddListener(OnRefuseButtonClicked);
        }

        private void OnDisable()
        {
            _confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
            _refuseButton.onClick.RemoveListener(OnRefuseButtonClicked);
        }

        public void Show()
        {

        }

        public void Hide()
        {

        }

        private void OnConfirmButtonClicked()
        {

        }

        private void OnRefuseButtonClicked()
        {

        }
    }
}
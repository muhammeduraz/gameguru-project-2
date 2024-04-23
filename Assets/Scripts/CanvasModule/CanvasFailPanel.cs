using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace Assets.Scripts.CanvasModule
{
    public class CanvasFailPanel : BasePanel
    {
        #region Variables

        [Header("Components")]
        [SerializeField] protected Button _retryButton;
        [SerializeField] protected GameObject _failText;

        #endregion Variables

        #region Functions

        public override void Initialize()
        {
            base.Initialize();

            _failText.transform.localScale = Vector3.zero;

            _retryButton.transform.localScale = Vector3.zero;
            _retryButton.enabled = false;
        }

        public override void Dispose()
        {
            base.Dispose();

            _retryButton = null;
        }

        protected async override Task OnAppear()
        {
            await base.OnAppear();
            await _failText.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack).AsyncWaitForCompletion();
            await Task.Delay(500);
            await _retryButton.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack).AsyncWaitForCompletion();
        }

        protected async override Task OnDisappear()
        {
            _retryButton.enabled = false;
            await base.OnDisappear();

            _failText.transform.localScale = Vector3.zero;
            _retryButton.transform.localScale = Vector3.zero;
        }

        private void InitializeRetryButton()
        {
            _retryButton.onClick.RemoveAllListeners();
            _retryButton.onClick.AddListener(OnRetryButtonClicked);
            _retryButton.enabled = true;
        }

        private void TerminateRetryButton()
        {
            _retryButton.enabled = false;
            _retryButton.onClick.RemoveAllListeners();
        }

        private void OnRetryButtonClicked()
        {

        }

        #endregion Functions
    }
}
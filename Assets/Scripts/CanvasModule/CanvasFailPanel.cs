using Zenject;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace Assets.Scripts.CanvasModule
{
    public class CanvasFailPanel : BasePanel
    {
        #region Variables

        private SignalBus _signalBus;

        [Header("Components")]
        [SerializeField] protected Button _retryButton;
        [SerializeField] protected GameObject _failText;

        #endregion Variables

        #region Functions

        [Inject]
        private void CanvasFailPanelMonoConstructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

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

            _failText = null;
            _signalBus = null;
            _retryButton = null;
        }

        protected async override Task OnAppear()
        {
            await base.OnAppear();

            _failText.transform.localScale = Vector3.zero;
            _retryButton.transform.localScale = Vector3.zero;

            await _failText.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack).AsyncWaitForCompletion();
            await Task.Delay(1500);

            InitializeRetryButton();
            await _retryButton.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack).AsyncWaitForCompletion();
        }

        protected async override Task OnDisappear()
        {
            TerminateRetryButton();
            await base.OnDisappear();

            _failText.transform.localScale = Vector3.zero;
            _retryButton.transform.localScale = Vector3.zero;
        }

        private void InitializeRetryButton()
        {
            if (_retryButton == null) return;

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
            _signalBus.Fire<RetryButtonClickedSignal>();
            TerminateRetryButton();
        }

        #endregion Functions
    }
}
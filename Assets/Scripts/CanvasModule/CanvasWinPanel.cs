using Zenject;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace Assets.Scripts.CanvasModule
{
    public class CanvasWinPanel : BasePanel
    {
        #region Variables

        private SignalBus _signalBus;

        [Header("Components")]
        [SerializeField] protected Button _continueButton;
        [SerializeField] protected GameObject _successText;

        #endregion Variables

        #region Functions

        [Inject]
        private void CanvasWinPanelMonoConstructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Initialize()
        {
            base.Initialize();

            _successText.transform.localScale = Vector3.zero;
            
            _continueButton.transform.localScale = Vector3.zero;
            _continueButton.enabled = false;
        }

        public override void Dispose()
        {
            base.Dispose();

            _signalBus = null;
            _successText = null;
            _continueButton = null;
        }

        protected async override Task OnAppear()
        {
            await base.OnAppear();
            await _successText.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack).AsyncWaitForCompletion();
            await Task.Delay(500);

            InitializeContinueButton();
            await _continueButton.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack).AsyncWaitForCompletion();
        }

        protected async override Task OnDisappear()
        {
            TerminateContinueButton();
            await base.OnDisappear();

            _successText.transform.localScale = Vector3.zero;
            _continueButton.transform.localScale = Vector3.zero;
        }

        private void InitializeContinueButton()
        {
            _continueButton.onClick.RemoveAllListeners();
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _continueButton.enabled = true;
        }

        private void TerminateContinueButton()
        {
            _continueButton.enabled = false;
            _continueButton.onClick.RemoveAllListeners();
        }

        private void OnContinueButtonClicked()
        {
            _signalBus.Fire<ContinueButtonClickedSignal>();
            TerminateContinueButton();
        }

        #endregion Functions
    }
}
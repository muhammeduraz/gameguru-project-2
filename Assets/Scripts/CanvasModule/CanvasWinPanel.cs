using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace Assets.Scripts.CanvasModule
{
    public class CanvasWinPanel : BasePanel
    {
        #region Variables

        [Header("Components")]
        [SerializeField] protected Button _continueButton;
        [SerializeField] protected GameObject _successText;

        #endregion Variables

        #region Functions

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

            _continueButton = null;
        }

        protected async override Task OnAppear()
        {
            await base.OnAppear();
            await _successText.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack).AsyncWaitForCompletion();
            await Task.Delay(500);
            await _continueButton.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack).AsyncWaitForCompletion();
        }

        protected async override Task OnDisappear()
        {
            _continueButton.enabled = false;
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
            
        }

        #endregion Functions
    }
}
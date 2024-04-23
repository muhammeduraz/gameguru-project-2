using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CanvasModule
{
    public class CanvasWinPanel : BasePanel
    {
        #region Variables

        [Header("Components")]
        [SerializeField] protected Button _continueButton;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public override void Initialize()
        {
            base.Initialize();

            _continueButton.enabled = false;
        }

        public override void Dispose()
        {
            base.Dispose();

            _continueButton = null;
        }

        protected override void OnAppear()
        {

        }

        protected override void OnDisappear()
        {

        }

        private void InitializeContinueButton()
        {
            _continueButton.onClick.RemoveAllListeners();
            _continueButton.onClick.AddListener(OnNextButtonClicked);
            _continueButton.enabled = true;
        }

        private void TerminateContinueButton()
        {
            _continueButton.enabled = false;
            _continueButton.onClick.RemoveAllListeners();
        }

        private void OnNextButtonClicked()
        {
            
        }

        #endregion Functions
    }
}
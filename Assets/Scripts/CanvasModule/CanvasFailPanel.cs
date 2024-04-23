using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CanvasModule
{
    public class CanvasFailPanel : BasePanel
    {
        #region Variables

        [Header("Components")]
        [SerializeField] protected Button _retryButton;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public override void Initialize()
        {
            base.Initialize();

            _retryButton.enabled = false;
        }

        public override void Dispose()
        {
            base.Dispose();

            _retryButton = null;
        }

        protected override void OnAppear()
        {

        }

        protected override void OnDisappear()
        {

        }

        #endregion Functions
    }
}
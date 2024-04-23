using Zenject;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace Assets.Scripts.CanvasModule
{
    public class CanvasStartPanel : BasePanel
    {
        #region Variables

        private SignalBus _signalBus;

        [Header("Components")]
        [SerializeField] protected Button _startButton;
        [SerializeField] protected GameObject _tapToStartText;

        #endregion Variables

        #region Functions

        [Inject]
        private void CanvasStartPanelMonoConstructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Initialize()
        {
            base.Initialize();

            canvasGroup.alpha = 1.0f;
            _tapToStartText.transform.localScale = Vector3.one;
            _startButton.transform.localScale = Vector3.one;
            InitializeStartButton();

            Appear();
        }

        public override void Dispose()
        {
            base.Dispose();

            _signalBus = null;
            _startButton = null;
            _tapToStartText = null;
        }

        protected async override Task OnAppear()
        {
            await base.OnAppear();

            _tapToStartText.transform.localScale = Vector3.one;

            _startButton.transform.localScale = Vector3.one;
            InitializeStartButton();
        }

        protected async override Task OnDisappear()
        {
            TerminateStartButton();
            await base.OnDisappear();

            _tapToStartText.transform.localScale = Vector3.zero;
            _startButton.transform.localScale = Vector3.zero;
        }

        private void InitializeStartButton()
        {
            _startButton.onClick.RemoveAllListeners();
            _startButton.onClick.AddListener(OnStartButtonClicked);
            _startButton.enabled = true;
        }

        private void TerminateStartButton()
        {
            _startButton.enabled = false;
            _startButton.onClick.RemoveAllListeners();
        }

        private void OnStartButtonClicked()
        {
            _signalBus.Fire<StartButtonClickedSignal>();
            TerminateStartButton();
            Disappear();
        }

        #endregion Functions
    }
}
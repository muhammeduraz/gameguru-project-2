using Assets.Scripts.InputModule;
using Assets.Scripts.CanvasModule;

namespace Assets.Scripts.StateModule
{
    public class StartState : BaseState
    {
        #region Variables

        private CustomInput _customInput;
        private CanvasManager _canvasManager;

        #endregion Variables

        #region Functions

        public StartState(CustomInput customInput, CanvasManager canvasManager) : base()
        {
            _customInput = customInput;
            _canvasManager = canvasManager;
        }

        public override void Dispose()
        {
            base.Dispose();

            _customInput = null;
            _canvasManager = null;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            _customInput.Disable();
            _canvasManager.Appear(typeof(CanvasStartPanel));
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        #endregion Functions
    }
}
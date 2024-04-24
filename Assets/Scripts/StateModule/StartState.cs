using Assets.Scripts.InputModule;
using Assets.Scripts.CanvasModule;
using Assets.Scripts.PlayerModule;

namespace Assets.Scripts.StateModule
{
    public class StartState : BaseState
    {
        #region Variables

        private Player _player;
        private CustomInput _customInput;
        private CanvasManager _canvasManager;

        #endregion Variables

        #region Functions

        public StartState(Player player, CustomInput customInput, CanvasManager canvasManager) : base()
        {
            _player = player;
            _customInput = customInput;
            _canvasManager = canvasManager;
        }

        public override void Dispose()
        {
            base.Dispose();

            _player = null;
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
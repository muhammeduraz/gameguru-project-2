using Assets.Scripts.InputModule;
using Assets.Scripts.CanvasModule;
using Assets.Scripts.PlayerModule;
using Assets.Scripts.CubeModule;

namespace Assets.Scripts.StateModule
{
    public class StartState : BaseState
    {
        #region Variables

        private Player _player;
        private CubePlacer _cubePlacer;
        private CustomInput _customInput;
        private CanvasManager _canvasManager;

        #endregion Variables

        #region Functions

        public StartState(Player player, CubePlacer cubePlacer, CustomInput customInput, CanvasManager canvasManager) : base()
        {
            _player = player;
            _cubePlacer = cubePlacer;
            _customInput = customInput;
            _canvasManager = canvasManager;
        }

        public override void Dispose()
        {
            base.Dispose();

            _player = null;
            _cubePlacer = null;
            _customInput = null;
            _canvasManager = null;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            _customInput.Disable();
            _cubePlacer.DisableFallCube();

            _canvasManager.Appear(typeof(CanvasStartPanel));
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        #endregion Functions
    }
}
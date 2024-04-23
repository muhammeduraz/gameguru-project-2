using Assets.Scripts.CubeModule;
using Assets.Scripts.InputModule;
using Assets.Scripts.CanvasModule;

namespace Assets.Scripts.StateModule
{
    public class WinState : BaseState
    {
        #region Variables

        private CubePlacer _cubePlacer;
        private CustomInput _customInput;
        private CanvasManager _canvasManager;

        #endregion Variables

        #region Functions

        public WinState(CustomInput customInput, CanvasManager canvasManager, CubePlacer cubePlacer) : base()
        {
            _cubePlacer = cubePlacer;
            _customInput = customInput;
            _canvasManager = canvasManager;
        }

        public override void Dispose()
        {
            base.Dispose();

            _cubePlacer = null;
            _customInput = null;
            _canvasManager = null;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            _cubePlacer.Disable();
            _customInput.Disable();
            _canvasManager.Appear(typeof(CanvasWinPanel));
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        #endregion Functions
    }
}
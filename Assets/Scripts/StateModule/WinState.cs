using Assets.Scripts.CubeModule;
using Assets.Scripts.InputModule;
using Assets.Scripts.CanvasModule;
using Assets.Scripts.FinishModule;

namespace Assets.Scripts.StateModule
{
    public class WinState : BaseState
    {
        #region Variables

        private CubePlacer _cubePlacer;
        private CustomInput _customInput;
        private CanvasManager _canvasManager;
        private FinishManager _finishManager;

        #endregion Variables

        #region Functions

        public WinState(CustomInput customInput, CanvasManager canvasManager, CubePlacer cubePlacer, FinishManager finishManager) : base()
        {
            _cubePlacer = cubePlacer;
            _customInput = customInput;
            _canvasManager = canvasManager;
            _finishManager = finishManager;
        }

        public override void Dispose()
        {
            base.Dispose();

            _cubePlacer = null;
            _customInput = null;
            _canvasManager = null;
            _finishManager = null;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            _cubePlacer.Disable();
            _customInput.Disable();
            _canvasManager.Appear(typeof(CanvasWinPanel));
            _finishManager.PlaceNewFinishLine();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        #endregion Functions
    }
}
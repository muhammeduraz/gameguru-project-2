using Assets.Scripts.CubeModule;
using Assets.Scripts.InputModule;
using Assets.Scripts.CanvasModule;
using Assets.Scripts.FinishModule;

namespace Assets.Scripts.StateModule
{
    public class FailState : BaseState
    {
        #region Variables

        private CubePlacer _cubePlacer;
        private CustomInput _customInput;
        private CanvasManager _canvasManager;
        private FinishManager _finishManager;

        #endregion Variables

        #region Functions

        public FailState(CustomInput customInput, CanvasManager canvasManager, CubePlacer cubePlacer, FinishManager finishManager) : base()
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
            _finishManager.MoveCurrentFinishLine();
            _canvasManager.Appear(typeof(CanvasFailPanel));
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        #endregion Functions
    }
}
using Assets.Scripts.CubeModule;
using Assets.Scripts.InputModule;
using Assets.Scripts.CanvasModule;
using Assets.Scripts.PlayerModule;
using Assets.Scripts.CameraModule;

namespace Assets.Scripts.StateModule
{
    public class StartState : BaseState
    {
        #region Variables

        private Player _player;
        private CubePlacer _cubePlacer;
        private CustomInput _customInput;
        private CanvasManager _canvasManager;
        private CameraManager _cameraManager;

        #endregion Variables

        #region Functions

        public StartState(Player player, CubePlacer cubePlacer, CustomInput customInput, CanvasManager canvasManager, CameraManager cameraManager) : base()
        {
            _player = player;
            _cubePlacer = cubePlacer;
            _customInput = customInput;
            _canvasManager = canvasManager;
            _cameraManager = cameraManager;
        }

        public override void Dispose()
        {
            base.Dispose();

            _player = null;
            _cubePlacer = null;
            _customInput = null;
            _canvasManager = null;
            _cameraManager = null;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            _customInput.Disable();

            _player.OnStartStateEnter(_cubePlacer.PreviousCube);

            _cameraManager.PlayerCamera.AttachCameraTarget();

            _cubePlacer.DisableFallCube();
            _cubePlacer.ScalePreviousCubeBackToInitialSize();

            _canvasManager.Appear(typeof(CanvasStartPanel));
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        #endregion Functions
    }
}
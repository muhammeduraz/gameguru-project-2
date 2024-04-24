using Assets.Scripts.CubeModule;
using Assets.Scripts.InputModule;
using Assets.Scripts.CanvasModule;
using Assets.Scripts.FinishModule;
using Assets.Scripts.CameraModule;
using Assets.Scripts.PlayerModule;

namespace Assets.Scripts.StateModule
{
    public class WinState : BaseState
    {
        #region Variables

        private Player _player;
        private CubePlacer _cubePlacer;
        private CustomInput _customInput;
        private CanvasManager _canvasManager;
        private FinishManager _finishManager;
        private CameraManager _cameraManager;

        #endregion Variables

        #region Functions

        public WinState(Player player, CustomInput customInput, CanvasManager canvasManager, CubePlacer cubePlacer, FinishManager finishManager, CameraManager cameraManager) : base()
        {
            _player = player;
            _cubePlacer = cubePlacer;
            _customInput = customInput;
            _canvasManager = canvasManager;
            _finishManager = finishManager;
            _cameraManager = cameraManager;
        }

        public override void Dispose()
        {
            base.Dispose();

            _player = null;
            _cubePlacer = null;
            _customInput = null;
            _canvasManager = null;
            _finishManager = null;
            _cameraManager = null;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            _cubePlacer.Disable();
            _customInput.Disable();
            _finishManager.PlaceNewFinishLine();

            _player.PlayerAnimation.PlayDanceAnimation();

            _cameraManager.OrbitalCamera.Activate();
            _cameraManager.OrbitalCamera.StartOrbitalCameraSequence();

            _canvasManager.Appear(typeof(CanvasWinPanel));
        }

        public override void OnStateExit()
        {
            base.OnStateExit();

            _player.PlayerAnimation.StopDanceAnimation();

            _cameraManager.OrbitalCamera.StopOrbitalCameraSequence();
            _cameraManager.OrbitalCamera.Deactivate();
        }

        #endregion Functions
    }
}
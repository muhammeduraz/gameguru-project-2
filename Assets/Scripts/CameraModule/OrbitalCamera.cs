using Zenject;
using Cinemachine;
using DG.Tweening;
using Assets.Scripts.PlayerModule;

namespace Assets.Scripts.CameraModule
{
    public class OrbitalCamera : BaseCamera
    {
        #region Variables

        private Tween _orbitalTween;

        private SignalBus _signalBus;
        private CinemachineOrbitalTransposer _orbitalTransposer;

        #endregion Variables

        #region Functions

        public OrbitalCamera(SignalBus signalBus, [Inject(Id = "OrbitalCamera")] CinemachineVirtualCamera baseCamera, Player player) : base(baseCamera, player)
        {
            _signalBus = signalBus;
            _orbitalTransposer = baseCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        }

        public override void Initialize()
        {
            base.Initialize();

            baseCamera.enabled = false;

            baseCamera.m_Follow = cameraTarget;
            baseCamera.m_LookAt = cameraTarget;

            Activate();
            StartOrbitalCameraSequence();
        }

        public override void Dispose()
        {
            base.Dispose();

            _orbitalTween = null;
            _orbitalTransposer = null;

            _signalBus = null;
        }

        private void StartOrbitalCameraSequence()
        {
            _orbitalTween?.Kill();
            _orbitalTween = DOTween.To(() => _orbitalTransposer.m_XAxis.Value, x => _orbitalTransposer.m_XAxis.Value = x, 360f, 4f)
                .SetRelative()
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }

        private void StopOrbitalCameraSequence()
        {
            _orbitalTween?.Kill();
            _orbitalTransposer.m_XAxis.Value = 0f;
        }

        #endregion Functions
    }
}
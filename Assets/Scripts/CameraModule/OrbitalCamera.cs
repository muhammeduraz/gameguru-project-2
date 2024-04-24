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

        private CinemachineOrbitalTransposer _orbitalTransposer;

        #endregion Variables

        #region Functions

        public OrbitalCamera([Inject(Id = "OrbitalCamera")] CinemachineVirtualCamera baseCamera, Player player) : base(baseCamera, player)
        {
            _orbitalTransposer = baseCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        }

        public override void Initialize()
        {
            base.Initialize();

            baseCamera.enabled = false;

            baseCamera.m_Follow = cameraTarget;
            baseCamera.m_LookAt = cameraTarget;
        }

        public override void Dispose()
        {
            base.Dispose();

            _orbitalTween = null;
            _orbitalTransposer = null;
        }

        public void StartOrbitalCameraSequence()
        {
            _orbitalTransposer.m_XAxis.Value = 0f;

            _orbitalTween?.Kill();
            _orbitalTween = DOTween.To(() => _orbitalTransposer.m_XAxis.Value, x => _orbitalTransposer.m_XAxis.Value = x, 360f, 4f)
                .SetRelative()
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }

        public void StopOrbitalCameraSequence()
        {
            _orbitalTween?.Kill();
        }

        #endregion Functions
    }
}
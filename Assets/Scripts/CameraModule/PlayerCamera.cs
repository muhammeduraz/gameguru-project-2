using Zenject;
using Cinemachine;
using Assets.Scripts.PlayerModule;

namespace Assets.Scripts.CameraModule
{
    public class PlayerCamera : BaseCamera
    {
        #region Functions

        public PlayerCamera([Inject(Id = "PlayerCamera")] CinemachineVirtualCamera baseCamera, Player player) : base(baseCamera, player)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            AttachCameraTarget();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void AttachCameraTarget()
        {
            baseCamera.m_Follow = cameraTarget;
            baseCamera.m_LookAt = cameraTarget;
        }

        public void DetachCameraTarget(int durationInMilliseconds)
        {
            baseCamera.m_Follow = null;
            baseCamera.m_LookAt = null;
        }

        #endregion Functions
    }
}
using Zenject;
using Cinemachine;
using Assets.Scripts.PlayerModule;

namespace Assets.Scripts.CameraModule
{
    public class PlayerCamera : BaseCamera
    {
        #region Variables



        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public PlayerCamera([Inject(Id = "PlayerCamera")] CinemachineVirtualCamera baseCamera, Player player) : base(baseCamera, player)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            baseCamera.m_Follow = cameraTarget;
            baseCamera.m_LookAt = cameraTarget;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion Functions
    }
}
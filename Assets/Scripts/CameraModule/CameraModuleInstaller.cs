using Zenject;

namespace Assets.Scripts.CameraModule
{
    public class CameraModuleInstaller : MonoInstaller
    {
        #region Functions

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerCamera>().AsSingle();
            Container.BindInterfacesAndSelfTo<OrbitalCamera>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraManager>().AsSingle();
        }

        #endregion Functions
    }
}
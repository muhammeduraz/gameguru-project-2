using Zenject;

namespace Assets.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        #region Functions

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        }

        #endregion Functions
    }
}
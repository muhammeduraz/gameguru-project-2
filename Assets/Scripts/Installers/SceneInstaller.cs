using Zenject;
using Assets.Scripts.FinishModule;

namespace Assets.Scripts.Installers
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        #region Functions

        public override void InstallBindings()
        {
            FinishModuleSignalInstaller.Install(Container);
        }

        #endregion Functions
    }
}
using Zenject;
using Assets.Scripts.FinishModule;
using Assets.Scripts.CubeModule.Signals;
using Assets.Scripts.ParticleModule.Signals;

namespace Assets.Scripts.Installers
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        #region Functions

        public override void InstallBindings()
        {
            CubeModuleSignalInstaller.Install(Container);
            FinishModuleSignalInstaller.Install(Container);
            ParticleModuleSignalInstaller.Install(Container);
        }

        #endregion Functions
    }
}
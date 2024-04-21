using Zenject;
using Assets.Scripts.FinishModule;
using Assets.Scripts.ParticleModule.Signals;

namespace Assets.Scripts.Installers
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        #region Functions

        public override void InstallBindings()
        {
            FinishModuleSignalInstaller.Install(Container);
            ParticleModuleSignalInstaller.Install(Container);
        }

        #endregion Functions
    }
}
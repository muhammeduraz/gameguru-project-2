using Zenject;

namespace Assets.Scripts.ParticleModule.Signals
{
    public class ParticleModuleSignalInstaller : Installer<ParticleModuleSignalInstaller>
    {
        #region Functions

        public override void InstallBindings()
        {
            Container.DeclareSignal<ParticleRequestSignal>();
        }

        #endregion Functions
    }
}
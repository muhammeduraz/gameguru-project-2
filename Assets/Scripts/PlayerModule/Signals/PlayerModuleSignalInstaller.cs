using Zenject;

namespace Assets.Scripts.PlayerModule.Signals
{
    public class PlayerModuleSignalInstaller : Installer<PlayerModuleSignalInstaller>
    {
        #region Functions

        public override void InstallBindings()
        {
            Container.DeclareSignal<PlayerMovementEndedSignal>();
            Container.DeclareSignal<PlayerMovementStartedSignal>();
        }

        #endregion Functions
    }
}
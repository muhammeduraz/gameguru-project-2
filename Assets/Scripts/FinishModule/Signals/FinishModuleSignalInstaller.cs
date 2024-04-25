using Zenject;

namespace Assets.Scripts.FinishModule
{
    public class FinishModuleSignalInstaller : Installer<FinishModuleSignalInstaller>
    {
        #region Functions

        public override void InstallBindings()
        {
            Container.DeclareSignal<GameFailSignal>();
            Container.DeclareSignal<GameWinSignal>();
        }

        #endregion Functions
    }
}
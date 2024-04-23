using Zenject;

namespace Assets.Scripts.CanvasModule
{
    public class CanvasModuleSignalInstaller : Installer<CanvasModuleSignalInstaller>
    {
        #region Functions

        public override void InstallBindings()
        {
            Container.DeclareSignal<ContinueButtonClickedSignal>();
            Container.DeclareSignal<RetryButtonClickedSignal>();
            Container.DeclareSignal<StartButtonClickedSignal>();
        }

        #endregion Functions
    }
}
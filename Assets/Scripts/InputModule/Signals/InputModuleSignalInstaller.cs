using Zenject;

namespace Assets.Scripts.InputModule
{
    public class InputModuleSignalInstaller : Installer<InputModuleSignalInstaller>
    {
        #region Functions

        public override void InstallBindings()
        {
            Container.DeclareSignal<InputTapSignal>();
            Container.DeclareSignal<ChangeInputStateSignal>();
        }

        #endregion Functions
    }
}
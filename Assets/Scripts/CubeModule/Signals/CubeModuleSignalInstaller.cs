using Zenject;

namespace Assets.Scripts.CubeModule.Signals
{
    public class CubeModuleSignalInstaller : Installer<CubeModuleSignalInstaller>
    {
        #region Functions

        public override void InstallBindings()
        {
            Container.DeclareSignal<CubePlacedSignal>();
        }

        #endregion Functions
    }
}
using Zenject;
using Assets.Scripts.InputModule;
using Assets.Scripts.TimerModule;
using Assets.Scripts.StateModule;
using Assets.Scripts.FinishModule;
using Assets.Scripts.PlayerModule;
using Assets.Scripts.CanvasModule;
using Assets.Scripts.CubeModule.Signals;
using Assets.Scripts.PlayerModule.Signals;
using Assets.Scripts.ParticleModule.Signals;

namespace Assets.Scripts.Installers
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        #region Functions

        public override void InstallBindings()
        {
            CubeModuleSignalInstaller.Install(Container);
            InputModuleSignalInstaller.Install(Container);
            PlayerModuleSignalInstaller.Install(Container);
            FinishModuleSignalInstaller.Install(Container);
            CanvasModuleSignalInstaller.Install(Container);
            ParticleModuleSignalInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<Timer>().AsTransient();
            Container.BindInterfacesAndSelfTo<CanvasManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<Player>().FromComponentInHierarchy().AsSingle();

            StateModuleInstaller.Install(Container);
        }

        #endregion Functions
    }
}
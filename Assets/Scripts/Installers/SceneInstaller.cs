using Zenject;
using Assets.Scripts.TimerModule;
using Assets.Scripts.InputModule;
using Assets.Scripts.FinishModule;
using Assets.Scripts.PlayerModule;
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
            InputModuleSignalInstaller.Install(Container);
            FinishModuleSignalInstaller.Install(Container);
            ParticleModuleSignalInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<Timer>().AsTransient();

            Container.BindInterfacesAndSelfTo<Player>().FromComponentInHierarchy().AsSingle();
        }

        #endregion Functions
    }
}
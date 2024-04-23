using Zenject;

namespace Assets.Scripts.StateModule
{
    public class StateModuleInstaller : Installer<StateModuleInstaller>
    {
        #region Functions

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WinState>().AsSingle();
            Container.BindInterfacesAndSelfTo<FailState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<StartState>().AsSingle();

            Container.BindInterfacesAndSelfTo<StateHandler>().AsSingle();
        }

        #endregion Functions
    }
}
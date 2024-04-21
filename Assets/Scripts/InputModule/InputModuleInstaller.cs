using Zenject;
using UnityEngine;
using Assets.Scripts.InputModule.Data;

namespace Assets.Scripts.InputModule
{
    public class InputModuleInstaller : MonoInstaller<InputModuleInstaller>
    {
        #region Variables

        [SerializeField] private CustomInputSettingsSO _customInputSettingsSO;

        #endregion Variables

        #region Functions

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CustomInput>().AsSingle().WithArguments(_customInputSettingsSO);
        }

        #endregion Functions
    }
}
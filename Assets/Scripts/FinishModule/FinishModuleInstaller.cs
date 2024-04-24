using Zenject;
using UnityEngine;

namespace Assets.Scripts.FinishModule
{
    public class FinishModuleInstaller : MonoInstaller<FinishModuleInstaller>
    {
        #region Variables

        [Header("References")]
        [SerializeField] private Finish _finishPrefab;
        [SerializeField] private FinishSettingsSO _finishSettingsSO;

        #endregion Variables

        #region Functions

        public override void InstallBindings()
        {
            Container.BindMemoryPool<Finish, FinishPool>().FromComponentInNewPrefab(_finishPrefab);
            Container.BindInterfacesAndSelfTo<FinishManager>().AsSingle().WithArguments(_finishSettingsSO);
        }

        #endregion Functions
    }
}
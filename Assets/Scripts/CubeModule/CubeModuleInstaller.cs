using Zenject;
using UnityEngine;
using Assets.Scripts.CubeModule.Data;

namespace Assets.Scripts.CubeModule
{
    public class CubeModuleInstaller : MonoInstaller<CubeModuleInstaller>
    {
        #region Variables

        [Header("References")]
        [SerializeField] private Cube _cubePrefab;
        [SerializeField] private CubeColorDataSO _cubeColorDataSO;
        [SerializeField] private CubePlacerSettingsSO _cubePlacerDataSO;

        #endregion Variables

        #region Functions

        public override void InstallBindings()
        {
            Container.BindInstance(_cubeColorDataSO);
            Container.BindMemoryPool<Cube, CubePool>().FromComponentInNewPrefab(_cubePrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<CubePlacer>().AsSingle().WithArguments(_cubePlacerDataSO);
        }

        #endregion Functions
    }
}
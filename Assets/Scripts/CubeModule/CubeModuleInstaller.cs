using Zenject;
using UnityEngine;

namespace Assets.Scripts.CubeModule
{
    public class CubeModuleInstaller : MonoInstaller<CubeModuleInstaller>
    {
        #region Variables

        [Header("References")]
        [SerializeField] private Cube _cubePrefab;

        #endregion Variables

        #region Functions

        public override void InstallBindings()
        {
            Container.BindMemoryPool<Cube, CubePool>().FromComponentInNewPrefab(_cubePrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<CubePlacer>().AsSingle();
        }

        #endregion Functions
    }
}
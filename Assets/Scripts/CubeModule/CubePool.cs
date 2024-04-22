using Zenject;
using Assets.Scripts.CubeModule.Data;

namespace Assets.Scripts.CubeModule
{
    public class CubePool : MonoMemoryPool<Cube>
    {
        #region Variables

        private CubeColorDataSO _cubeColorDataSO;
        
        #endregion Variables
        
        #region Functions

        public CubePool(CubeColorDataSO cubeColorDataSO)
        {
            _cubeColorDataSO = cubeColorDataSO;
        }

        protected override void OnCreated(Cube cube)
        {
            base.OnCreated(cube);
        }

        protected override void OnDespawned(Cube cube)
        {
            base.OnDespawned(cube);
        }

        protected override void OnDestroyed(Cube cube)
        {
            base.OnDestroyed(cube);
        }

        protected override void OnSpawned(Cube cube)
        {
            base.OnSpawned(cube);

            cube.DeactivateRigidbody();
            cube.ChangeMaterial(_cubeColorDataSO.GetRandomMaterial());
        }

        protected override void Reinitialize(Cube cube)
        {
            base.Reinitialize(cube);
        }

        #endregion Functions
    }
}
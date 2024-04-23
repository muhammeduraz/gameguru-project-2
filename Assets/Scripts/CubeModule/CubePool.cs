using Zenject;
using System.Collections.Generic;
using Assets.Scripts.CubeModule.Data;

namespace Assets.Scripts.CubeModule
{
    public class CubePool : MonoMemoryPool<Cube>
    {
        #region Variables

        private int x = 0;

        private int _maxAliveCubeCount = 15;

        private Cube _cacheCube;
        private Queue<Cube> _cubeQueue;
        private CubeColorDataSO _cubeColorDataSO;
        
        #endregion Variables
        
        #region Functions

        public CubePool(CubeColorDataSO cubeColorDataSO)
        {
            _cubeQueue = new Queue<Cube>();
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

            cube.name = x++.ToString();

            _cubeQueue.Enqueue(cube);
            cube.ResetRotation();
            cube.DeactivateRigidbody();
            cube.ChangeMaterial(_cubeColorDataSO.GetRandomMaterial());

            DespawnIfPossible();
        }

        protected override void Reinitialize(Cube cube)
        {
            base.Reinitialize(cube);
        }

        private void DespawnIfPossible()
        {
            if (_cubeQueue.Count > _maxAliveCubeCount)
            {
                _cacheCube = _cubeQueue.Dequeue();
                Despawn(_cacheCube);
            }
        }

        #endregion Functions
    }
}
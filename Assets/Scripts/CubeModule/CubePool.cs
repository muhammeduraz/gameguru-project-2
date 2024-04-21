using Zenject;

namespace Assets.Scripts.CubeModule
{
    public class CubePool : MonoMemoryPool<Cube>
    {
        #region Functions

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
        }

        protected override void Reinitialize(Cube cube)
        {
            base.Reinitialize(cube);
        }

        #endregion Functions
    }
}
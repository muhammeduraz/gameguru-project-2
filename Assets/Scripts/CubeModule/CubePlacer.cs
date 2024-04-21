using System;
using Zenject;

namespace Assets.Scripts.CubeModule
{
    public class CubePlacer : IInitializable, ITickable, IDisposable
    {
        #region Variables

        private CubePool _cubePool;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public CubePlacer(CubePool cubePool)
        {
            _cubePool = cubePool;
        }

        public void Initialize()
        {

        }

        public void Tick()
        {
            
        }

        public void Dispose()
        {
            
        }

        #endregion Functions
    }
}
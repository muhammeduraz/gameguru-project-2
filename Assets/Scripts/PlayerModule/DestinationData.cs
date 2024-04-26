using UnityEngine;

namespace Assets.Scripts.PlayerModule
{
    public struct DestinationData
    {
        #region Variables

        public Vector3 cubeSize;
        public Vector3 destination;

        #endregion Variables
        
        #region Functions

        public DestinationData(Vector3 cubeSize, Vector3 destination)
        {
            this.cubeSize = cubeSize;
            this.destination = destination;
        }

        #endregion Functions
    }
}
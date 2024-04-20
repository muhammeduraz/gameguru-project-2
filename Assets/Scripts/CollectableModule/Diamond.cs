using UnityEngine;

namespace Assets.Scripts.Collectables
{
    public class Diamond : BaseCollectable
    {
        #region Variables



        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public override void OnInteract()
        {
            Disable();
        }

        #endregion Functions
    }
}
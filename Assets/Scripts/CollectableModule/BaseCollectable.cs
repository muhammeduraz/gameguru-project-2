using UnityEngine;
using Assets.Scripts.Interfaces.Interaction;

namespace Assets.Scripts.Collectables
{
    public abstract class BaseCollectable : MonoBehaviour, IInteractable
    {
        #region Variables

        [Header("Components")]
        [SerializeField] protected GameObject mesh;
        [SerializeField] protected Collider collectableCollider;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        protected void Disable()
        {
            collectableCollider.enabled = false;
            gameObject.SetActive(false);
        }

        public abstract void OnInteract();

        #endregion Functions
    }
}
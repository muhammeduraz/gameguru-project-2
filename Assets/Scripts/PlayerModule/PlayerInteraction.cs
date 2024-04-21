using System;
using Zenject;
using UnityEngine;
using Assets.Scripts.Interfaces.Interaction;

namespace Assets.Scripts.PlayerModule
{
    public class PlayerInteraction : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        private IInteractable _cacheInteractable;

        [Header("Components")]
        [SerializeField] private Collider _collider;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public void Initialize()
        {

        }

        public void Dispose()
        {

        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            otherCollider.TryGetComponent(out _cacheInteractable);
            if (_cacheInteractable == null) return;

            _cacheInteractable.OnInteract();
        }

        #endregion Functions
    }
}
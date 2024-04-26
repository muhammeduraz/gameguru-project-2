using System;
using Zenject;
using UnityEngine;
using Assets.Scripts.Interfaces.Interaction;

namespace Assets.Scripts.PlayerModule
{
    public class PlayerInteraction : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        private bool _isActive;

        private IInteractable _cacheInteractable;

        [Header("Components")]
        [SerializeField] private Collider _collider;

        #endregion Variables

        #region Functions

        public void Initialize() { }

        public void Dispose() { }

        public void Enable() => _isActive = true;
        public void Disable() => _isActive = false;

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (!_isActive) return;

            otherCollider.TryGetComponent(out _cacheInteractable);
            if (_cacheInteractable == null) return;

            _cacheInteractable.OnInteract();
        }

        #endregion Functions
    }
}
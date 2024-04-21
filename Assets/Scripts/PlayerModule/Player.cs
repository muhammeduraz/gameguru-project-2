using System;
using Zenject;
using UnityEngine;

namespace Assets.Scripts.PlayerModule
{
    public class Player : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        [Header("References")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private PlayerInteraction _playerInteraction;

        [Header("Components")]
        [SerializeField] private Transform _transform;

        #endregion Variables

        #region Properties

        public Transform Transform { get => _transform; }

        #endregion Properties

        #region Functions

        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }

        #endregion Functions
    }
}
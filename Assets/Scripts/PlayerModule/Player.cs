using System;
using Zenject;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        [Header("References")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private PlayerInteraction _playerInteraction;

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

        #endregion Functions
    }
}
using System;
using Zenject;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private float _movementSpeed;

        [Header("Components")]
        [SerializeField] private Transform _transform;

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
        
        public void Move(Vector3 targetPosition)
        {
            
        }

        #endregion Functions
    }
}
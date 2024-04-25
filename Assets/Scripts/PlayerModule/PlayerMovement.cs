using System;
using Zenject;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.CubeModule;
using System.Collections.Generic;

namespace Assets.Scripts.PlayerModule
{
    public class PlayerMovement : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        private bool _isMoving;

        private Cube _previousCube;

        private Tween _tween;

        private Queue<Vector3> _destinationQueue;
        
        [Header("Settings")]
        [SerializeField] private float _rotationDuration = 0.1f;
        [SerializeField] private float _movementDuration = 0.2f;

        [Header("Components")]
        [SerializeField] private Transform _transform;

        [Header("References")]
        [SerializeField] private PlayerAnimation _playerAnimation;

        #endregion Variables

        #region Functions

        public void Initialize() 
        {
            _isMoving = false;

            _destinationQueue = new Queue<Vector3>();
        }

        public void Dispose() 
        {
            _tween?.Kill();
            _tween = null;

            _transform = null;
        }

        private void OnMovementStart()
        {
            _isMoving = true;
            _playerAnimation.PlayRunAnimation();
        }

        private void OnMovementEnd()
        {
            _isMoving = false;
            _playerAnimation.PlayIdleAnimation();
        }

        public void AddDestination(Cube cube)
        {
            Vector3 destination = _transform.position;

            if (_previousCube != null)
            {
                destination = new Vector3(cube.Position.x, _transform.position.y, _transform.position.z + _previousCube.Size.z / 2.0f);
                _destinationQueue.Enqueue(destination);
            }

            destination = cube.transform.position;
            _destinationQueue.Enqueue(destination);

            StartMovementSequence();

            _previousCube = cube;
        }

        public void StartMovementSequence()
        {
            if (_isMoving) return;

            OnMovementStart();
            Move();
        }

        public async void Move()
        {
            while (_destinationQueue.Count > 0)
            {
                Vector3 destination = _destinationQueue.Dequeue();

                Vector3 dir = destination - _transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                _transform.DORotateQuaternion(lookRotation, _rotationDuration);

                _tween?.Kill();
                _tween = _transform.DOMove(destination, _movementDuration).SetEase(Ease.Linear);

                await _tween.AsyncWaitForCompletion();
            }

            OnMovementEnd();
        }

        #endregion Functions
    }
}
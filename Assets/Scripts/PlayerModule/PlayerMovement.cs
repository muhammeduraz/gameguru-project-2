using System;
using Zenject;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using Assets.Scripts.CubeModule;
using System.Collections.Generic;

namespace Assets.Scripts.PlayerModule
{
    public class PlayerMovement : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        private bool _isMoving;

        private Cube _previousCube;
        private WaitForSeconds _waitForSeconds;

        private Tween _tween;

        private Queue<Vector3> _destinationQueue;
        
        [Header("Settings")]
        [SerializeField] private float _rotationDuration = 0.1f;
        [SerializeField] private float _movementDuration = 0.2f;

        [Header("Components")]
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody _rigidbody;

        [Header("References")]
        [SerializeField] private PlayerAnimation _playerAnimation;

        #endregion Variables

        #region Functions

        public void Initialize() 
        {
            _isMoving = false;

            _waitForSeconds = new WaitForSeconds(4.0f);
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
            _playerAnimation.StopIdleAnimation();
            _playerAnimation.PlayRunAnimation();
        }

        private void OnMovementEnd()
        {
            _isMoving = false;
            _playerAnimation.StopRunAnimation();
        }

        public void AddDestination(Cube cube)
        {
            Vector3 destination = _transform.position;

            if (_previousCube != null)
                destination = new Vector3(cube.Position.x, _transform.position.y, _previousCube.Position.z + _previousCube.Size.z / 2.0f);
            else
                destination = new Vector3(cube.Position.x, _transform.position.y, _transform.position.z + cube.Size.z / 2.0f);

            _destinationQueue.Enqueue(destination);
            _previousCube = cube;

            destination = cube.transform.position;
            _destinationQueue.Enqueue(destination);

            StartMovementSequence();
        }

        public void StartMovementSequence()
        {
            if (_isMoving) return;

            OnMovementStart();
            Move();
        }

        public void StopMovementSequence()
        {
            _isMoving = false;
        }

        public async void Move()
        {
            while (_destinationQueue.Count > 0)
            {
                if (!_isMoving) break;

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

        public void FallRigidbody()
        {
            StartCoroutine(FallRigidbodyCoroutine());
        }

        public IEnumerator FallRigidbodyCoroutine()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.velocity = 10.0f * Vector3.forward;

            yield return _waitForSeconds;

            ResetRigidbody();
        }

        public void ResetRigidbody()
        {
            StopCoroutine(FallRigidbodyCoroutine());

            _rigidbody.isKinematic = true;
            _rigidbody.transform.rotation = Quaternion.identity;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        #endregion Functions
    }
}
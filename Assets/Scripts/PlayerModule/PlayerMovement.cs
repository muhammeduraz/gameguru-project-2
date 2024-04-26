using System;
using Zenject;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;
using Assets.Scripts.CubeModule;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Assets.Scripts.FinishModule;

namespace Assets.Scripts.PlayerModule
{
    public class PlayerMovement : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        private bool _isMoving;

        private Cube _previousCube;
        private SignalBus _signalBus;
        private WaitForSeconds _waitForSeconds;

        private Tween _tween;

        private Queue<DestinationData> _destinationQueue;
        
        [Header("Settings")]
        [SerializeField] private float _rotationDuration = 0.1f;
        [SerializeField] private float _movementDuration = 0.2f;
        [SerializeField] private CubePlacerSettingsSO _cubeSettings;

        [Header("Components")]
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody _rigidbody;

        [Header("References")]
        [SerializeField] private PlayerAnimation _playerAnimation;

        #endregion Variables

        #region Functions

        [Inject]
        private void PlayerMovementMonoConstructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize() 
        {
            _isMoving = false;

            _waitForSeconds = new WaitForSeconds(4.0f);
            _destinationQueue = new Queue<DestinationData>();
        }

        public void Dispose() 
        {
            _tween?.Kill();
            _tween = null;

            _signalBus = null;
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
            _rigidbody.velocity = new Vector3(0.0f, _rigidbody.velocity.y, 0.0f);
            _playerAnimation.StopRunAnimation();
        }

        public void AddDestination(Cube cube)
        {
            DestinationData destinationData;
            destinationData.destination = _transform.position;

            if (_previousCube != null)
            {
                destinationData.destination = new Vector3(cube.Position.x, _transform.position.y, _previousCube.Position.z + _previousCube.Size.z / 2.0f);
                destinationData.cubeSize = _previousCube.Size;
            }
            else
            {
                destinationData.destination = new Vector3(cube.Position.x, _transform.position.y, _transform.position.z + cube.Size.z / 2.0f);
                destinationData.cubeSize = cube.Size;
            }

            _destinationQueue.Enqueue(destinationData);
            _previousCube = cube;

            destinationData.destination = cube.transform.position;
            destinationData.cubeSize = cube.Size;
            _destinationQueue.Enqueue(destinationData);

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

                DestinationData destinationData = _destinationQueue.Dequeue();
                Vector3 direction = (destinationData.destination - _transform.position).normalized;

                float distance = Vector3.Distance(_transform.position, destinationData.destination);
                float speed = distance / _movementDuration;

                Vector3 speedVector = direction * speed;
                speedVector.y = _rigidbody.velocity.y;

                Vector3 dir = destinationData.destination - _transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                _transform.DORotateQuaternion(lookRotation, _rotationDuration);

                _rigidbody.velocity = speedVector;

                await Task.Delay(Mathf.RoundToInt(_movementDuration * 1000));

                if (destinationData.cubeSize.x <= _cubeSettings.FailSizeLimit)
                {
                    FailGame();
                    break;
                }
            }

            OnMovementEnd();
        }

        private void FailGame()
        {
            _signalBus.Fire<GameFailSignal>();
        }

        public void FallRigidbody()
        {
            StartCoroutine(FallRigidbodyCoroutine());
        }

        public IEnumerator FallRigidbodyCoroutine()
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.angularVelocity = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));

            yield return _waitForSeconds;

            ResetRigidbody();
        }

        public void ResetRigidbody()
        {
            StopCoroutine(FallRigidbodyCoroutine());

            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.transform.rotation = Quaternion.identity;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        public void ResetDestinationData(Cube cube)
        {
            _previousCube = cube;
            _destinationQueue.Clear();
        }

        #endregion Functions
    }
}
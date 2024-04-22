using System;
using Zenject;
using UnityEngine;
using Unity.Mathematics;
using Assets.Scripts.InputModule;
using Assets.Scripts.CubeModule.Signals;

namespace Assets.Scripts.CubeModule
{
    public class CubePlacer : IInitializable, ITickable, IDisposable
    {
        #region Variables

        private bool _isActive;
        private bool _isMovingRight;

        private float _failSizeLimit;
        private float _currentZPosition;
        private float _correctPlacementThreshold;

        private float _movementSpeed;
        private float2 _movementRange;

        private Vector3 _currentCubeSize;

        private Cube _currentCube;
        private Cube _previousCube;
        private Cube _cacheFallCube;

        private CubePool _cubePool;
        private SignalBus _signalBus;

        private CubePlacedSignal _cubePlacedSignal;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public CubePlacer(SignalBus signalBus, CubePool cubePool, [Inject(Id = "InitialCube")] Cube initialCube)
        {
            _isActive = true;
            _isMovingRight = true;

            _failSizeLimit = 0.3f;
            _currentZPosition = 5f;

            _correctPlacementThreshold = 0.2f;

            _movementSpeed = 5.0f;
            _movementRange = new float2(-7.0f, 7.0f);

            _currentCubeSize = initialCube.Size;

            _cubePool = cubePool;
            _signalBus = signalBus;

            _previousCube = initialCube;

            _cubePlacedSignal = new CubePlacedSignal();
        }

        public void Initialize()
        {
            SpawnCube();

            _signalBus.Subscribe<InputTapSignal>(OnInputTapSignalFired);
        }

        public void Tick()
        {
            if (!_isActive) return;

            Vector3 targetPosition = GetTargetPosition();
            MoveCube(targetPosition);

            if (IsTargetPositionReached(targetPosition))
                _isMovingRight = !_isMovingRight;
        }

        public void Dispose()
        {
            _currentCube = null;
            _previousCube = null;

            _signalBus.Unsubscribe<InputTapSignal>(OnInputTapSignalFired);
            _signalBus = null;
        }

        private Vector3 GetTargetPosition()
        {
            Vector3 targetPosition = _currentZPosition * Vector3.forward + _movementRange.x * Vector3.right;
            if (_isMovingRight)
                targetPosition = _currentZPosition * Vector3.forward + _movementRange.y * Vector3.right;

            return targetPosition;
        }

        private void MoveCube(Vector3 targetPosition)
        {
            _currentCube.Position = Vector3.MoveTowards(_currentCube.Position, targetPosition, _movementSpeed * Time.deltaTime);
        }

        private bool IsTargetPositionReached(Vector3 targetPosition)
        {
            return Mathf.Approximately(_currentCube.Position.x, targetPosition.x);
        }

        private void SpawnCube()
        {
            if (!_isActive) return;

            _currentCube = _cubePool.Spawn();
            _currentCube.Size = _currentCubeSize;
            _currentCube.Position = _movementRange.x * Vector3.right + _currentZPosition * Vector3.forward;
        }

        private void FireCubePlacedSignal(bool correctly = false)
        {
            _cubePlacedSignal.Correctly = correctly;
            _cubePlacedSignal.Cube = _currentCube;

            _signalBus.Fire(_cubePlacedSignal);
        }

        private void OnInputTapSignalFired()
        {
            float differenceInX = Mathf.Abs(_previousCube.Position.x - _currentCube.Position.x);
            if (differenceInX <= _correctPlacementThreshold)
                OnPlacedCorrectly();
            else
                OnPlaced();

            _currentZPosition += _previousCube.Size.z;
            _previousCube = _currentCube;
            _isMovingRight = true;

            SpawnCube();
        }

        private void OnPlaced()
        {
            float differenceInX = _currentCube.Position.x - _previousCube.Position.x;
            if (Mathf.Abs(differenceInX) >= _previousCube.Size.x)
            {
                _isActive = false;
                _currentCube.ActivateRigidbodyAndDeactivateAsAsync();
                // Fail the game
                return;
            }

            // Set new cube size
            _currentCube.Size = GetNewCubeSize(differenceInX);
            // Set new cube position
            _currentCube.Position = GetNewCubePosition(differenceInX);

            // Create fall cube
            _cacheFallCube = _cubePool.Spawn();
            _cacheFallCube.ChangeMaterial(_currentCube.MeshRenderer.material);
            _cacheFallCube.ActivateRigidbodyAndDeactivateAsAsync();

            // Set fall cube size
            _cacheFallCube.Size = GetFallCubeSize();
            // Set fall cube position
            _cacheFallCube.Position = GetFallCubePosition(_cacheFallCube.Size.x, differenceInX);

            _currentCubeSize = _currentCube.Size;

            if (_currentCube.Size.x <= _failSizeLimit)
            {
                // Fail the game
                _isActive = false;
                return;
            }

            FireCubePlacedSignal();
        }

        private void OnPlacedCorrectly()
        {
            Vector3 currentCubePosition = _currentCube.Position;
            currentCubePosition.x = _previousCube.Position.x;
            _currentCube.Position = currentCubePosition;

            FireCubePlacedSignal(true);
        }

        private Vector3 GetNewCubeSize(float differenceInX)
        {
            float newCubeSizeX = _previousCube.Size.x - Mathf.Abs(differenceInX);
            Vector3 newCubeSize = _previousCube.Size;
            newCubeSize.x = newCubeSizeX;

            return newCubeSize;
        }

        private Vector3 GetNewCubePosition(float differenceInX)
        {
            float newCenterX = _previousCube.Position.x + differenceInX / 2f;
            Vector3 newCubePosition = _currentCube.Position;
            newCubePosition.x = newCenterX;

            return newCubePosition;
        }

        private Vector3 GetFallCubeSize()
        {
            float fallCubeSizeX = _previousCube.Size.x - _currentCube.Size.x;
            Vector3 fallCubeSize = _previousCube.Size;
            fallCubeSize.x = fallCubeSizeX;

            return fallCubeSize;
        }

        private Vector3 GetFallCubePosition(float fallCubeSizeX, float differenceInX)
        {
            float divider = 2f * Mathf.Sign(-differenceInX);
            float fallCubePositionX = _currentCube.Position.x - _currentCube.Size.x / divider - fallCubeSizeX / divider;
            Vector3 fallCubePosition = _currentCube.Position;
            fallCubePosition.x = fallCubePositionX;

            return fallCubePosition;
        }

        #endregion Functions
    }
}
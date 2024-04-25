using System;
using Zenject;
using UnityEngine;
using Unity.Mathematics;
using Assets.Scripts.InputModule;
using Assets.Scripts.FinishModule;
using Assets.Scripts.CubeModule.Signals;

namespace Assets.Scripts.CubeModule
{
    public class CubePlacer : IInitializable, ITickable, IDisposable
    {
        #region Variables

        private bool _isActive;
        private bool _isMovingRight;
        private bool _fallCubeDisableStatus;

        private float _currentZPosition;
        private float2 _movementRange;

        private Vector3 _initialCubeSize;
        private Vector3 _currentCubeSize;

        private Cube _currentCube;
        private Cube _previousCube;
        private Cube _cacheFallCube;

        private CubePool _cubePool;
        private SignalBus _signalBus;
        private CubePlacerSettingsSO _settings;

        private CubePlacedSignal _cubePlacedSignal;

        #endregion Variables

        #region Functions

        public CubePlacer(SignalBus signalBus, CubePool cubePool, CubePlacerSettingsSO settings)
        {
            _isActive = false;
            _isMovingRight = true;

            _cubePool = cubePool;
            _signalBus = signalBus;
            _settings = settings;

            _initialCubeSize = _settings.InitialCubeSize;

            _currentZPosition = _settings.InitialCubeSize.z;
            _movementRange = _settings.DefaultMovementRange;

            _currentCubeSize = _initialCubeSize;

            _cubePlacedSignal = new CubePlacedSignal();
        }

        public void Enable() => _isActive = true;
        public void Disable() => _isActive = false;

        public void Initialize()
        {
            SpawnCube();
            _currentCube.transform.position = Vector3.zero;

            _previousCube = _currentCube;
            _currentCube = null;

            _signalBus.Subscribe<InputTapSignal>(OnInputTapSignalFired);
        }

        public void Reinitialize()
        {
            _movementRange = new float2(_previousCube.Position.x + _settings.DefaultMovementRange.x, _previousCube.Position.x + _settings.DefaultMovementRange.y);
            _currentCubeSize = _initialCubeSize;
            SpawnCube();
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

        public void DisableCurrentCube()
        {
            if (_currentCube != null)
            {
                _currentCube.DisableAsLastCube();
                _currentCube = null;
            }
        }

        public void DisableFallCube()
        {
            if (_cacheFallCube != null && _fallCubeDisableStatus)
            {
                _fallCubeDisableStatus = false;
                _cacheFallCube.DisableAsLastCube();
            }
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
            _currentCube.Position = Vector3.MoveTowards(_currentCube.Position, targetPosition, _settings.MovementSpeed * Time.deltaTime);
        }

        private bool IsTargetPositionReached(Vector3 targetPosition)
        {
            return Mathf.Approximately(_currentCube.Position.x, targetPosition.x);
        }

        private void SpawnCube()
        {
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

        private void FireGameFailedSignal()
        {
            _signalBus.Fire<GameFailSignal>();
        }

        private bool DoesCubePlacedCorrectly(float differenceInX)
        {
            return differenceInX <= _settings.CorrectPlacementThreshold;
        }

        private bool DoesCurrentAndPreviousCubesAlign(float differenceInX)
        {
            return ((Mathf.Abs(differenceInX) >= _previousCube.Size.x && _currentCube.Size.x <= _previousCube.Size.x)
                || (_currentCube.Size.x > _previousCube.Size.x && Mathf.Abs(differenceInX) >= _currentCube.Size.x / 2f + _previousCube.Size.x / 2f));
        }

        private void OnInputTapSignalFired()
        {
            float differenceInX = Mathf.Abs(_previousCube.Position.x - _currentCube.Position.x);

            bool doesCurrentAndPreviousCubesAlign = DoesCurrentAndPreviousCubesAlign(differenceInX);
            if (doesCurrentAndPreviousCubesAlign)
            {
                OnCubeDoesNotAlignWithPreviousOne();
                return;
            }

            bool doesCubePlacedCorrectly = DoesCubePlacedCorrectly(differenceInX);
            if (doesCubePlacedCorrectly)
                OnPlacedCorrectly();
            else
                OnPlaced();
        }

        private bool DoesCurrentCubeSizeFallsUnderFailLimit()
        {
            return _currentCube.Size.x <= _settings.FailSizeLimit;
        }

        private bool IsCurrentCubeIsSmallerOrEqualThanPreviousOne()
        {
            return _currentCube.Size.x <= _previousCube.Size.x || Mathf.Approximately(_currentCube.Size.x, _previousCube.Size.x);
        }

        private void HandleCubePlacement()
        {
            float differenceInX = _currentCube.Position.x - _previousCube.Position.x;

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

            UpdateCurrentCubeSize();
            UpdateMovementRange();
        }

        private void OnPlaced()
        {
            bool isCurrentCubeIsSmallerOrEqualThanPreviousOne = IsCurrentCubeIsSmallerOrEqualThanPreviousOne();
            if (isCurrentCubeIsSmallerOrEqualThanPreviousOne)
                HandleCubePlacement();

            bool doesCurrentCubeSizeFallsUnderFailLimit = DoesCurrentCubeSizeFallsUnderFailLimit();
            if (doesCurrentCubeSizeFallsUnderFailLimit)
            {
                OnSizeFallsUnderFailLimit();
                return;
            }

            FireCubePlacedSignal();
            OnAfterPlacement();
        }

        private void OnSizeFallsUnderFailLimit()
        {
            Disable();
            _cacheFallCube = _currentCube;
            _currentCube = null;
            _fallCubeDisableStatus = true;
            FireGameFailedSignal();
        }

        private void OnCubeDoesNotAlignWithPreviousOne()
        {
            Disable();
            _currentCube.ActivateRigidbodyAndDeactivateAsAsync();
            _currentCube = null;
            FireGameFailedSignal();
        }

        private void OnPlacedCorrectly()
        {
            Vector3 currentCubePosition = _currentCube.Position;
            currentCubePosition.x = _previousCube.Position.x;
            _currentCube.Position = currentCubePosition;

            FireCubePlacedSignal(true);
            OnAfterPlacement();
        }

        private void OnAfterPlacement()
        {
            _previousCube = _currentCube;
            _currentZPosition += _previousCube.Size.z;
            _isMovingRight = true;

            SpawnCube();
        }

        private void UpdateCurrentCubeSize()
        {
            _currentCubeSize = _currentCube.Size;
        }

        private void UpdateMovementRange()
        {
            float rangeX = _currentCube.Position.x - _currentCube.Size.x - 1f;
            float rangeY = _currentCube.Position.x + _currentCube.Size.x + 1f;
            _movementRange = new float2(rangeX, rangeY);
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
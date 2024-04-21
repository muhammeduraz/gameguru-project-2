using System;
using Zenject;
using UnityEngine;
using Unity.Mathematics;
using Assets.Scripts.InputModule;

namespace Assets.Scripts.CubeModule
{
    public class CubePlacer : IInitializable, ITickable, IDisposable
    {
        #region Variables

        private bool _isActive;
        private bool _isMovingRight;

        private Cube _currentCube;
        private Cube _previousCube;

        private CubePool _cubePool;
        private SignalBus _signalBus;

        private float2 _movementRange;

        private float _currentCubeSize;
        private float _currentZPosition;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public CubePlacer(SignalBus signalBus, CubePool cubePool)
        {
            _isActive = true;

            _currentCubeSize = 5f;
            _currentZPosition = 5f;
            
            _movementRange = new float2(-10.0f, 10.0f);

            _cubePool = cubePool;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            SpawnCube();

            _signalBus.Subscribe<InputTapSignal>(OnInputTapSignalFired);
        }

        public void Tick()
        {
            if (!_isActive) return;

            Vector3 targetPosition = _currentZPosition * Vector3.forward + _movementRange.x * Vector3.right;
            if (_isMovingRight)
                targetPosition = _currentZPosition * Vector3.forward + _movementRange.y * Vector3.right;

            _currentCube.transform.position = Vector3.MoveTowards(_currentCube.transform.position, targetPosition, 15.0f * Time.deltaTime);
            if (_currentCube.transform.position == targetPosition)
            {
                _isMovingRight = !_isMovingRight;
            }
        }

        public void Dispose()
        {
            _currentCube = null;
            _previousCube = null;

            _signalBus.Subscribe<InputTapSignal>(OnInputTapSignalFired);
            _signalBus = null;
        }

        private void SpawnCube()
        {
            _currentCube = _cubePool.Spawn();
            _currentCube.transform.localScale = new Vector3(_currentCubeSize, 1f, _currentCubeSize);
            _currentCube.transform.position = _movementRange.x * Vector3.left + _currentZPosition * Vector3.forward;
        }

        private void OnInputTapSignalFired()
        {
            _currentZPosition += 5.0f;

            _previousCube = _currentCube;
            SpawnCube();
        }

        #endregion Functions
    }
}
using System;
using Zenject;
using UnityEngine;
using Assets.Scripts.PlayerModule;

namespace Assets.Scripts.CameraModule
{
    public class CameraManager : IInitializable, IDisposable
    {
        #region Variables

        private Camera _mainCamera;
        private PlayerCamera _playerCamera;
        private OrbitalCamera _orbitalCamera;

        #endregion Variables

        #region Properties

        public PlayerCamera PlayerCamera { get => _playerCamera; }
        public OrbitalCamera OrbitalCamera { get => _orbitalCamera; }

        #endregion Properties

        #region Functions

        public CameraManager(Player player, [Inject(Id = "MainCamera")] Camera mainCamera, PlayerCamera playerCamera, OrbitalCamera orbitalCamera)
        {
            _mainCamera = mainCamera;
            _playerCamera = playerCamera;
            _orbitalCamera = orbitalCamera;
        }

        public void Initialize() { }

        public void Dispose()
        {
            _mainCamera = null;
            _playerCamera = null;
            _orbitalCamera = null;
        }

        #endregion Functions
    }
}
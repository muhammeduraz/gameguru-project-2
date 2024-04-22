using System;
using Zenject;
using UnityEngine;
using Assets.Scripts.CubeModule;
using Assets.Scripts.CubeModule.Signals;
using Assets.Scripts.PlayerModule.Signals;

namespace Assets.Scripts.PlayerModule
{
    public class Player : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        private SignalBus _signalBus;

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

        [Inject]
        private void PlayerMonoConstructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _playerMovement.Initialize();
            _playerAnimation.Initialize();
            _playerInteraction.Initialize();

            _signalBus.Subscribe<CubePlacedSignal>(OnCubePlacedSignalFired);
        }

        public void Dispose()
        {
            _playerMovement.Dispose();
            _playerAnimation.Dispose();
            _playerInteraction.Dispose();

            _signalBus.Unsubscribe<CubePlacedSignal>(OnCubePlacedSignalFired);
            _signalBus = null;
        }

        private void OnCubePlacedSignalFired(CubePlacedSignal cubePlacedSignal)
        {
            MovePlayerToCube(cubePlacedSignal.Cube);
        }

        private async void MovePlayerToCube(Cube cube)
        {
            _signalBus.Fire<PlayerMovementStartedSignal>();

            _playerAnimation.PlayRunAnimation();
            await _playerMovement.Move(cube.transform.position);
            _playerAnimation.PlayIdleAnimation();
            
            _signalBus.Fire<PlayerMovementEndedSignal>();
        }

        #endregion Functions
    }
}
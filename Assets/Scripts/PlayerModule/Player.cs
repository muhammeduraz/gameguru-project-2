using System;
using Zenject;
using UnityEngine;
using Assets.Scripts.CubeModule;
using Assets.Scripts.CubeModule.Signals;

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
        public Vector3 Position { get => _transform.position; set => _transform.position = value; }

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

        public void OnWinStateEnter()
        {
            _playerMovement.StopMovementSequence();
            _playerAnimation.PlayDanceAnimation();
        }

        public void OnWinStateExit()
        {
            _playerAnimation.PlayIdleAnimation();
        }

        public void OnFailStateEnter()
        {
            _playerMovement.StopMovementSequence();
            _playerMovement.FallRigidbody();
        }

        public void OnStartStateEnter(Cube cube)
        {
            ResetPlayer(cube);
        }

        public void ResetPlayer(Cube cube)
        {
            _playerMovement.ResetRigidbody();
            _playerAnimation.PlayIdleAnimation();

            if (cube != null)
                _transform.position = cube.transform.position;
        }

        private void OnCubePlacedSignalFired(CubePlacedSignal cubePlacedSignal)
        {
            AddDestionationToPlayerMovement(cubePlacedSignal.Cube);
        }

        private void AddDestionationToPlayerMovement(Cube cube)
        {
            _playerAnimation.PlayRunAnimation();
            _playerMovement.AddDestination(cube);
        }

        #endregion Functions
    }
}
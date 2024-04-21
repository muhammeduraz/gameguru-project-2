using System;
using Zenject;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.CubeModule.Signals;

namespace Assets.Scripts.PlayerModule
{
    public class PlayerMovement : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        private Tween _tween;
        private SignalBus _signalBus;

        [Header("Settings")]
        [SerializeField] private float _movementSpeed;

        [Header("Components")]
        [SerializeField] private Transform _transform;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        [Inject]
        private void PlayerMovementMonoConstructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<CubePlacedSignal>(OnCubePlacedSignalFired);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CubePlacedSignal>(OnCubePlacedSignalFired);
            _signalBus = null;
        }

        private void OnCubePlacedSignalFired(CubePlacedSignal cubePlacedSignal)
        {
            Move(cubePlacedSignal.Cube.transform.position);
        }

        public void Move(Vector3 position)
        {
            _tween?.Kill();
            _tween = _transform.DOMove(position, 0.5f).SetEase(Ease.Linear);
        }

        #endregion Functions
    }
}
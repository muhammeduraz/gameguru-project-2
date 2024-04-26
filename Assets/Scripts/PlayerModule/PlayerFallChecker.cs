using System;
using Zenject;
using UnityEngine;
using Assets.Scripts.FinishModule;
using Assets.Scripts.Interfaces.Interaction;

namespace Assets.Scripts.PlayerModule
{
    public class PlayerFallChecker : MonoBehaviour, IInteractable, IDisposable
    {
        #region Variables

        private SignalBus _signalBus;

        #endregion Variables

        #region Functions

        [Inject]
        private void PlayerFallCheckerMonoConstructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Dispose()
        {
            _signalBus = null;
        }

        public void OnInteract()
        {
            _signalBus.Fire<GameFailSignal>();
        }

        #endregion Functions
    }
}
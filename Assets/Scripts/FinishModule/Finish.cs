using Zenject;
using UnityEngine;
using Assets.Scripts.ParticleModule;
using Assets.Scripts.Interfaces.Interaction;
using Assets.Scripts.ParticleModule.Signals;

namespace Assets.Scripts.FinishModule
{
    public class Finish : MonoBehaviour, IInteractable
    {
        #region Variables

        private SignalBus _signalBus;

        [Header("Components")]
        [SerializeField] private Collider _collider;
        [SerializeField] private float _confettiParticleHeight = 1.5f;

        #endregion Variables
        
        #region Properties

        public Vector3 Position { get => transform.position; set => transform.position = value; }

        #endregion Properties

        #region Functions

        [Inject]
        private void FinishMonoConstructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void OnInteract()
        {
            _signalBus.Fire(new ParticleRequestSignal(typeof(ConfettiParticle), transform.position + _confettiParticleHeight * Vector3.up));
            _signalBus.Fire<FinishInteractSignal>();
        }

        public void Enable()
        {
            _collider.enabled = true;
        }

        public void Disable()
        {
            _collider.enabled = false;
        }

        #endregion Functions
    }
}
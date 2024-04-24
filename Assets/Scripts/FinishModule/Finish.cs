using Zenject;
using UnityEngine;
using Assets.Scripts.Interfaces.Interaction;

namespace Assets.Scripts.FinishModule
{
    public class Finish : MonoBehaviour, IInteractable
    {
        #region Variables

        private SignalBus _signalBus;

        [Header("Components")]
        [SerializeField] private Collider _collider;

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
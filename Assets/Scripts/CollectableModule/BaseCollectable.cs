using Zenject;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Interfaces.Interaction;

namespace Assets.Scripts.Collectables
{
    public abstract class BaseCollectable : MonoBehaviour, IInteractable
    {
        #region Variables

        private Tween _rotationTween;
        protected SignalBus signalBus;

        [Header("Components")]
        [SerializeField] private float _rotationDuration = 2.0f;

        [Header("Components")]
        [SerializeField] protected GameObject mesh;
        [SerializeField] protected Collider collectableCollider;

        #endregion Variables

        #region Unity Functions

        private void Awake()
        {
            PlayRotationAnimation();
        }

        #endregion Unity Functions

        #region Functions

        [Inject]
        private void BaseCollectableMonoConstructor(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        protected void Disable()
        {
            _rotationTween?.Kill();

            collectableCollider.enabled = false;
            gameObject.SetActive(false);
        }

        private void PlayRotationAnimation()
        {
            _rotationTween = transform.DORotate(360.0f * Vector3.up, _rotationDuration)
                .SetEase(Ease.Linear)
                .SetRelative()
                .SetLoops(-1);
        }

        public abstract void OnInteract();

        #endregion Functions
    }
}
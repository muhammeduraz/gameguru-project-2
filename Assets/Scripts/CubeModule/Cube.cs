using UnityEngine;
using System.Threading.Tasks;
using System;
using DG.Tweening;

namespace Assets.Scripts.CubeModule
{
    public class Cube : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private int _rigidbodyDeactivationDuration = 5;

        [Header("Components")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private BoxCollider _boxCollider;
        [SerializeField] private MeshRenderer _meshRenderer;

        #endregion Variables

        #region Properties

        public Vector3 Size { get => transform.localScale; set => transform.localScale = value; }
        public Vector3 Position { get => transform.position; set => transform.position = value; }
        public MeshRenderer MeshRenderer { get => _meshRenderer; }

        #endregion Properties
        
        #region Functions

        public void Scale(Vector3 scale, float duration)
        {
            transform.DOScale(scale, duration).SetEase(Ease.OutCubic);
        }

        public void ResetRotation()
        {
            transform.rotation = Quaternion.identity;
        }

        public void ChangeMaterial(Material material)
        {
            _meshRenderer.material = material;
        }

        public void ActivateRigidbody()
        {
            _rigidbody.isKinematic = false;
        }

        public void DeactivateRigidbody()
        {
            if (_rigidbody == null) return;
            _rigidbody.isKinematic = true;
        }

        public void ActivateRigidbodyAndDeactivateAsAsync()
        {
            ActivateRigidbody();
            DeactivateRigidbodyAsync();
        }

        public async void DisableAsLastCube()
        {
            await transform.DOScale(0.0f, 0.2f).SetEase(Ease.Flash).AsyncWaitForCompletion();
        }

        private async void DeactivateRigidbodyAsync()
        {
            await Task.Delay(_rigidbodyDeactivationDuration * 1000);
            DeactivateRigidbody();
        }

        #endregion Functions
    }
}
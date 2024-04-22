using UnityEngine;
using System.Threading.Tasks;

namespace Assets.Scripts.CubeModule
{
    public class Cube : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private int _rigidbodyDeactivationDuration = 10;

        [Header("Components")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private BoxCollider _boxCollider;
        [SerializeField] private MeshRenderer _meshRenderer;

        #endregion Variables

        #region Properties

        public Vector3 Size { get => transform.localScale; set => transform.localScale = value; }
        public Vector3 Position { get => transform.position; set => transform.position = value; }

        #endregion Properties

        #region Functions

        public void ActivateRigidbody()
        {
            _rigidbody.isKinematic = false;
            DeactivateRigidbodyAsync();
        }

        public void DeactivateRigidbody()
        {
            _rigidbody.isKinematic = true;
        }

        private async void DeactivateRigidbodyAsync()
        {
            await Task.Delay(_rigidbodyDeactivationDuration * 1000);
            DeactivateRigidbody();
        }

        #endregion Functions
    }
}
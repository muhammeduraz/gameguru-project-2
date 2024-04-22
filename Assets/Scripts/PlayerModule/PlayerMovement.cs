using System;
using Zenject;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace Assets.Scripts.PlayerModule
{
    public class PlayerMovement : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        private Tween _tween;
        
        [Header("Settings")]
        [SerializeField] private float _movementSpeed;

        [Header("Components")]
        [SerializeField] private Transform _transform;

        #endregion Variables

        #region Functions

        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }

        public async Task Move(Vector3 position)
        {
            _tween?.Kill();
            _tween = _transform.DOMove(position, 0.5f).SetEase(Ease.Linear);

            Vector3 dir = position - _transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            _transform.DORotateQuaternion(lookRotation, 0.1f);

            await _tween.AsyncWaitForCompletion();
        }

        #endregion Functions
    }
}
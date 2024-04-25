using System;
using Zenject;
using UnityEngine;

namespace Assets.Scripts.PlayerModule
{
    public class PlayerAnimation : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        private readonly int Run = Animator.StringToHash("Run");
        private readonly int Idle = Animator.StringToHash("Idle");
        private readonly int Dance = Animator.StringToHash("Dance");

        [Header("Components")]
        [SerializeField] private Animator _animator;

        #endregion Variables

        #region Functions

        public void Initialize() { }

        public void Dispose() { }

        public void PlayIdleAnimation()
        {
            StopRunAnimation();
            _animator.SetBool(Idle, true);
        }

        public void StopIdleAnimation()
        {
            _animator.SetBool(Idle, false);
        }

        public void PlayRunAnimation()
        {
            _animator.SetBool(Run, true);
        }

        public void StopRunAnimation()
        {
            _animator.SetBool(Run, false);
        }

        public void PlayDanceAnimation()
        {
            _animator.SetTrigger(Dance);
        }

        #endregion Functions
    }
}
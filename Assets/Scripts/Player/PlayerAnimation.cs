using System;
using Zenject;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimation : MonoBehaviour, IInitializable, IDisposable
    {
        #region Variables

        private readonly int Run = Animator.StringToHash("Run");
        private readonly int Dance = Animator.StringToHash("Dance");

        [Header("Components")]
        [SerializeField] private Animator _animator;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public void Initialize()
        {

        }

        public void Dispose()
        {

        }

        public void PlayIdleAnimation()
        {
            _animator.SetBool(Run, false);
            _animator.SetBool(Dance, false);
        }

        public void PlayRunAnimation()
        {
            _animator.SetBool(Run, true);
            _animator.SetBool(Dance, false);
        }

        public void PlayDanceAnimation()
        {
            _animator.SetBool(Run, false);
            _animator.SetBool(Dance, true);
        }

        #endregion Functions
    }
}
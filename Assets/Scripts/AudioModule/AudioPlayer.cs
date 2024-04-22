using System;
using Zenject;
using UnityEngine;

namespace Assets.Scripts.AudioModule
{
    public class AudioPlayer : IInitializable, IDisposable
    {
        #region Variables

        private AudioSource _audioSource;

        #endregion Variables

        #region Functions

        public AudioPlayer(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }

        public void Play(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        #endregion Functions
    }
}
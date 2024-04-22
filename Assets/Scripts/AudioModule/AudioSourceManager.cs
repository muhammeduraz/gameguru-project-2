using System;
using Zenject;
using UnityEngine;

namespace Assets.Scripts.AudioModule
{
    public class AudioSourceManager : IInitializable, IDisposable
    {
        #region Variables

        private AudioSource _audioSource;

        #endregion Variables

        #region Functions

        public AudioSourceManager(AudioSource audioSource)
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

        public void Play(AudioClip clip, float pitch)
        {
            _audioSource.pitch = pitch;
            Play(clip);
        }

        #endregion Functions
    }
}
using System;
using Zenject;
using UnityEngine;
using Assets.Scripts.FinishModule;
using Assets.Scripts.AudioModule.Data;
using Assets.Scripts.CubeModule.Signals;

namespace Assets.Scripts.AudioModule
{
    public class CubePlacementAudioPlayer : IInitializable, IDisposable
    {
        #region Variables

        private float _currentPitch;

        private AudioClip _audioClip;
        private SignalBus _signalBus;
        private AudioPlayerDataSO _audioPlayerDataSO;
        private AudioSourceManager _audioSourceManager;

        #endregion Variables

        #region Functions

        public CubePlacementAudioPlayer(SignalBus signalBus, AudioClip audioClip, AudioPlayerDataSO audioPlayerDataSO, AudioSourceManager audioSourceManager)
        {
            _signalBus = signalBus;
            _audioClip = audioClip;
            _audioPlayerDataSO = audioPlayerDataSO;
            _audioSourceManager = audioSourceManager;

            _currentPitch = _audioPlayerDataSO.DefaultPitch;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameWinSignal>(OnGameWinSignalFired);
            _signalBus.Subscribe<GameFailSignal>(OnGameFailSignalFired);
            _signalBus.Subscribe<CubePlacedSignal>(OnCubePlacedSignalFired);
        }

        public void Dispose()
        {
            _signalBus.Subscribe<GameWinSignal>(OnGameWinSignalFired);
            _signalBus.Subscribe<GameFailSignal>(OnGameFailSignalFired);
            _signalBus.Unsubscribe<CubePlacedSignal>(OnCubePlacedSignalFired);
            _signalBus = null;
        }

        private void OnGameWinSignalFired()
        {
            ResetPitch();
        }

        private void OnGameFailSignalFired()
        {
            ResetPitch();
        }

        private void ResetPitch()
        {
            _currentPitch = _audioPlayerDataSO.DefaultPitch;
        }

        private void OnCubePlacedSignalFired(CubePlacedSignal cubePlacedSignal)
        {
            UpdateAudioPitch(cubePlacedSignal.Correctly);

            if (!cubePlacedSignal.Correctly) return;

            PlayCorrectPlacementAudioClip();
        }

        private void PlayCorrectPlacementAudioClip()
        {
            _audioSourceManager.Play(_audioClip, _currentPitch);
        }

        private void UpdateAudioPitch(bool correctPlacement)
        {
            if (correctPlacement)
            {
                if (Mathf.Approximately(_currentPitch, _audioPlayerDataSO.MaxPitch))
                {
                    _currentPitch = _audioPlayerDataSO.DefaultPitch;
                    return;
                }

                _currentPitch += _audioPlayerDataSO.PitchStep;
            }
            else
            {
                _currentPitch = _audioPlayerDataSO.DefaultPitch;
            }
        }

        #endregion Functions
    }
}
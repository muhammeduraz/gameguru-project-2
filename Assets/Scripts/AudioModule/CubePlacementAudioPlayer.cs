using System;
using Zenject;
using UnityEngine;
using Assets.Scripts.AudioModule.Data;
using Assets.Scripts.CubeModule.Signals;

namespace Assets.Scripts.AudioModule
{
    public class CubePlacementAudioPlayer : IInitializable, IDisposable
    {
        #region Variables

        private int _correctPlacementComboCount;

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

            _correctPlacementComboCount = 0;
            _currentPitch = _audioPlayerDataSO.DefaultPitch;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<CubePlacedSignal>(OnCubePlacedSignalFired);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CubePlacedSignal>(OnCubePlacedSignalFired);
            _signalBus = null;
        }

        private void OnCubePlacedSignalFired(CubePlacedSignal cubePlacedSignal)
        {
            UpdateComboCount(cubePlacedSignal.Correctly);
            UpdateAudioPitch(cubePlacedSignal.Correctly);

            if (!cubePlacedSignal.Correctly) return;

            PlayCorrectPlacementAudioClip();
        }

        private void PlayCorrectPlacementAudioClip()
        {
            _audioSourceManager.Play(_audioClip, _currentPitch);
        }

        private void UpdateComboCount(bool correctPlacement)
        {
            if (correctPlacement)
            {
                _correctPlacementComboCount++;
            }
            else
            {
                _correctPlacementComboCount = 0;
            }
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
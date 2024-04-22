using UnityEngine;

namespace Assets.Scripts.AudioModule.Data
{
    [CreateAssetMenu(fileName = "AudioPlayerDataSO", menuName = "Scriptable Objects/AudioPlayerDataSO")]
    public class AudioPlayerDataSO : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private float _maxPitch;
        [SerializeField] private float _pitchStep;
        [SerializeField] private float _defaultPitch;

        #endregion Variables

        #region Properties

        public float MaxPitch { get => _maxPitch; }
        public float PitchStep { get => _pitchStep; }
        public float DefaultPitch { get => _defaultPitch; }

        #endregion Properties
    }
}
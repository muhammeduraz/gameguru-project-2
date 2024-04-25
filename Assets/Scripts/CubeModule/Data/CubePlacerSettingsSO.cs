using UnityEngine;
using Unity.Mathematics;

namespace Assets.Scripts.CubeModule
{
    [CreateAssetMenu(fileName = "CubePlacerSettingsSO", menuName = "Scriptable Objects/CubePlacerSettingsSO")]
    public class CubePlacerSettingsSO : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private float _failSizeLimit = 0.3f;
        [SerializeField] private float _scaleDurationOnSpawn = 0.1f;
        [SerializeField] private float _correctPlacementThreshold = 0.2f;

        [SerializeField] private float _movementSpeed = 5.0f;
        [SerializeField] private float2 _defaultMovementRange = new float2(-6.0f, 6.0f);

        [SerializeField] private Vector3 _initialCubeSize = new Vector3(5.0f, 0.0f, 5.0f);

        #endregion Variables

        #region Properties

        public float FailSizeLimit { get => _failSizeLimit; }
        public float ScaleDurationOnSpawn { get => _scaleDurationOnSpawn; }
        public float CorrectPlacementThreshold { get => _correctPlacementThreshold; }

        public float MovementSpeed { get => _movementSpeed; }
        public float2 DefaultMovementRange { get => _defaultMovementRange; }

        public Vector3 InitialCubeSize { get => _initialCubeSize; }

        #endregion Properties
    }
}
using UnityEngine;
using Unity.Mathematics;

namespace Assets.Scripts.CubeModule
{
    [CreateAssetMenu(fileName = "CubePlacerDataSO", menuName = "Scriptable Objects/CubePlacerDataSO")]
    public class CubePlacerDataSO : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private float _failSizeLimit = 0.3f;
        [SerializeField] private float _correctPlacementThreshold = 0.2f;

        [SerializeField] private float _movementSpeed = 5.0f;
        [SerializeField] private float2 _defaultMovementRange = new float2(-6.0f, 6.0f);

        #endregion Variables

        #region Properties

        public float FailSizeLimit { get => _failSizeLimit; }
        public float CorrectPlacementThreshold { get => _correctPlacementThreshold; }

        public float MovementSpeed { get => _movementSpeed; }
        public float2 DefaultMovementRange { get => _defaultMovementRange; }

        #endregion Properties
    }
}
using UnityEngine;

namespace Assets.Scripts.InputModule.Data
{
    [CreateAssetMenu(fileName = "CustomInputSettingsSO", menuName = "Scriptable Objects/CustomInputSettingsSO")]
    public class CustomInputSettingsSO : ScriptableObject
    {
        #region Variables

        [SerializeField] private float _tapTimeThreshold = 0.2f;
        [SerializeField] private float _tapDistanceThreshold = 5f;

        #endregion Variables

        #region Properties

        public float TapTimeThreshold { get => _tapTimeThreshold; }
        public float TapDistanceThreshold { get => _tapDistanceThreshold; }

        #endregion Properties
    }
}
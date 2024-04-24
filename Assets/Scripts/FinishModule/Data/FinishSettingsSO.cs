using UnityEngine;

namespace Assets.Scripts.FinishModule
{
    [CreateAssetMenu(fileName = "FinishSettingsSO", menuName = "Scriptable Objects/FinishSettingsSO")]
    public class FinishSettingsSO : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private float _finishLineDistance = 50.0f;

        #endregion Variables

        #region Properties

        public float FinishLineDistance { get => _finishLineDistance; }

        #endregion Properties
    }
}
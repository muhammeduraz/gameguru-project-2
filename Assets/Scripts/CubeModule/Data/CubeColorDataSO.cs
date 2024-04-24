using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.CubeModule.Data
{
    [CreateAssetMenu(fileName = "CubeColorDataSO", menuName = "Scriptable Objects/CubeColorDataSO")]
    public class CubeColorDataSO : ScriptableObject
    {
        #region Variables

        [Header("Colors")]
        [SerializeField] private List<Material> _materialList;

        #endregion Variables

        #region Functions

        public Material GetRandomMaterial()
        {
            return _materialList[Random.Range(0, _materialList.Count)];
        }

        #endregion Functions
    }
}
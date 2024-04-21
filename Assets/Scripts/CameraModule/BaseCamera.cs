using System;
using Zenject;
using UnityEngine;
using Cinemachine;
using Assets.Scripts.PlayerModule;

namespace Assets.Scripts.CameraModule
{
    public abstract class BaseCamera : IInitializable, IDisposable
    {
        #region Variables

        protected Transform cameraTarget;

        [Header("Components")]
        [SerializeField] protected CinemachineVirtualCamera baseCamera;

        #endregion Variables

        #region Functions

        public BaseCamera(CinemachineVirtualCamera baseCamera, Player player)
        {
            this.baseCamera = baseCamera;
            cameraTarget = player.Transform;
        }

        public virtual void Initialize()
        {
            
        }

        public virtual void Dispose()
        {
            baseCamera = null;
        }

        public void Activate()
        {
            baseCamera.enabled = true;
        }

        public void Deactivate()
        {
            baseCamera.enabled = false;
        }

        #endregion Functions
    }
}
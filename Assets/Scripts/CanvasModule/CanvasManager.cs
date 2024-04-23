using System;
using Zenject;
using UnityEngine;

namespace Assets.Scripts.CanvasModule
{
    public class CanvasManager : IInitializable, IDisposable
    {
        #region Variables

        private CanvasGroup _canvasGroup;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public CanvasManager([Inject(Id = "MainCanvasGroup")] CanvasGroup canvasGroup)
        {
            _canvasGroup = canvasGroup;
        }

        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }

        #endregion Functions
    }
}
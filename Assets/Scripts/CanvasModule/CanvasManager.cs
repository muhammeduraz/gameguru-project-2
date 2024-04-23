using System;
using Zenject;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.CanvasModule
{
    public class CanvasManager : IInitializable, IDisposable
    {
        #region Variables

        private CanvasGroup _canvasGroup;
        private List<IPanel> _panelList;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public CanvasManager([Inject(Id = "MainCanvasGroup")] CanvasGroup canvasGroup, List<IPanel> panelList)
        {
            _canvasGroup = canvasGroup;
            _panelList = panelList;
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
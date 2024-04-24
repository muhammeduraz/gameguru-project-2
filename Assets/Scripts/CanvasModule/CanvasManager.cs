using System;
using Zenject;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.CanvasModule
{
    public class CanvasManager : IInitializable, IDisposable
    {
        #region Variables

        private IPanel _currentPanel;
        private List<IPanel> _panelList;

        private CanvasGroup _canvasGroup;

        #endregion Variables

        #region Functions

        public CanvasManager(List<IPanel> panelList, [Inject(Id = "MainCanvasGroup")] CanvasGroup canvasGroup)
        {
            _currentPanel = null;

            _canvasGroup = canvasGroup;
            _panelList = panelList;
        }

        public void Initialize()
        {
            _canvasGroup.alpha = 1;
        }

        public void Dispose()
        {
            _currentPanel = null;
            _canvasGroup = null;
            _panelList = null;
        }

        private IPanel GetPanel(Type panelType)
        {
            return _panelList.Find(panel => panel.GetType() == panelType);
        }

        public void Appear(Type panelType)
        {
            if (_currentPanel != null)
                _currentPanel.DisappearAsync();

            _currentPanel = GetPanel(panelType);
            if (_currentPanel != null)
                _currentPanel.AppearAsync();
        }

        #endregion Functions
    }
}
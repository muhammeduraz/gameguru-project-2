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

        private CanvasGroup _canvasGroup;

        private List<IPanel> _panelList;

        #endregion Variables

        #region Functions

        public CanvasManager([Inject(Id = "MainCanvasGroup")] CanvasGroup canvasGroup, List<IPanel> panelList)
        {
            _canvasGroup = canvasGroup;
            _panelList = panelList;
        }

        public void Initialize()
        {
            Appear(typeof(CanvasStartPanel));
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
                _currentPanel.Disappear();

            _currentPanel = GetPanel(panelType);
            if (_currentPanel != null)
                _currentPanel.Appear();
        }

        public void DisappearAllPanels()
        {
            _panelList.ForEach(panel => panel.Disappear());
        }

        #endregion Functions
    }
}
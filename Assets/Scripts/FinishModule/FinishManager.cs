using System;
using Zenject;
using UnityEngine;
using Assets.Scripts.PlayerModule;

namespace Assets.Scripts.FinishModule
{
    public class FinishManager : IInitializable, IDisposable
    {
        #region Variables

        private Player _player;
        private Finish _currentFinish;
        private FinishPool _finishPool;
        private FinishSettingsSO _settings;

        #endregion Variables

        #region Functions

        public FinishManager(FinishSettingsSO settings, FinishPool finishPool, Player player)
        {
            _player = player;
            _settings = settings;
            _finishPool = finishPool;
        }

        public void Initialize()
        {
            PlaceNewFinishLine();
        }

        public void Dispose()
        {
            _settings = null;
            _finishPool = null;
            _currentFinish = null;
        }

        public void PlaceNewFinishLine()
        {
            _currentFinish = _finishPool.Spawn();
            _currentFinish.Position = _player.Position + _settings.FinishLineDistance * Vector3.forward;
        }

        public void MoveCurrentFinishLine()
        {
            _currentFinish.Position = _player.Position + _settings.FinishLineDistance * Vector3.forward;
        }

        #endregion Functions
    }
}
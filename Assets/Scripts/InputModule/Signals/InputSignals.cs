using UnityEngine;

namespace Assets.Scripts.InputModule
{
    #region Signals

    public struct ChangeInputStateSignal
    {
        private bool _activate;
        public bool Activate { get => _activate; }

        public ChangeInputStateSignal(bool activate)
        {
            _activate = activate;
        }
    }

    public struct InputTapSignal
    {
        private Vector3 _mousePosition;
        public Vector3 MousePosition { get => _mousePosition; }

        public InputTapSignal(Vector3 mousePosition)
        {
            _mousePosition = mousePosition;
        }
    }

    #endregion Signals
}
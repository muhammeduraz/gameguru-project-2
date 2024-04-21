using System;
using UnityEngine;

namespace Assets.Scripts.ParticleModule.Signals
{
    #region Signals

    public struct ParticleRequestSignal
    {
        private Type _type;
        private Vector3 _position;

        public Type Type { get => _type; }
        public Vector3 Position { get => _position; }

        public ParticleRequestSignal(Type type, Vector3 position)
        {
            _type = type;
            _position = position;
        }
    }

    #endregion Signals
}
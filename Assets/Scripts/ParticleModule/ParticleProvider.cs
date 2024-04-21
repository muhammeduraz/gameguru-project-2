using System;
using Zenject;
using UnityEngine;
using Assets.Scripts.ParticleModule.Signals;
using Assets.Scripts.ParticleModule.Particles;

namespace Assets.Scripts.ParticleModule
{
    public class ParticleProvider : IInitializable, IDisposable
    {
        #region Variables

        private Vector3 _positionOffset;

        private BaseParticle _cacheParticle;

        private ParticlePool _pool;
        private ParticleFactory _factory;

        private SignalBus _signalBus;

        #endregion Variables

        #region Functions

        public ParticleProvider(ParticlePool pool, ParticleFactory factory, SignalBus signalBus, Vector3 positionOffset) 
        {
            _pool = pool;
            _factory = factory;
            _signalBus = signalBus;
            _positionOffset = positionOffset;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ParticleRequestSignal>(OnParticleRequestSignalFired);
        }

        public void Dispose()
        {
            _pool = null;
            _factory = null;
            _cacheParticle = null;

            _signalBus.Unsubscribe<ParticleRequestSignal>(OnParticleRequestSignalFired);
            _signalBus = null;
        }

        private void OnParticleRequestSignalFired(ParticleRequestSignal particleRequestSignal)
        {
            _cacheParticle = _pool.GetParticle(particleRequestSignal.Type);
            if (_cacheParticle == null)
            {
                _cacheParticle = _factory.ManufactureParticle(particleRequestSignal.Type);
                if (_cacheParticle == null) return;

                _cacheParticle.OnTerminated += _pool.AddBackToPool;
            }

            _cacheParticle.SetPosition(particleRequestSignal.Position + _positionOffset);
            _cacheParticle.Play();
        }

        #endregion Functions
    }
}
using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.ParticleModule.Particles;

namespace Assets.Scripts.ParticleModule
{
    public class ParticleFactory : IDisposable
    {
        #region Variables

        private BaseParticle _cacheParticle;

        private Transform _particleParent;
        private List<BaseParticle> _particlePrefabList;

        #endregion Variables

        #region Functions

        public ParticleFactory(Transform particleParent, List<BaseParticle> particlePrefabList)
        {
            _particleParent = particleParent;
            _particlePrefabList = particlePrefabList;
        }

        public void Dispose()
        {
            _cacheParticle = null;
            _particleParent = null;
            _particlePrefabList = null;
        }

        public BaseParticle ManufactureParticle(Type type)
        {
            _cacheParticle = _particlePrefabList.Find(particle => particle.GetType() == type);
            if (_cacheParticle == null) return null;

            _cacheParticle = GameObject.Instantiate(_cacheParticle, _particleParent);
            return _cacheParticle;
        }

        #endregion Functions
    }
}